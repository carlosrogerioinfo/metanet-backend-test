using AutoMapper;
using Esterdigi.Api.Core.Commands;
using FluentValidator;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Caching;
using MetaNet.Microservices.Infrastructure.Transactions;

namespace MetaNet.Microservices.Service
{
    public class ProductService: Notifiable,
                ICommandHandler<ProductRegisterRequest>,
                ICommandHandler<ProductUpdateRequest>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDapperRepository _repositoryDapper;
        private readonly ISaleItemRepository _repositorySaleItem;
        private readonly ICacheRepository _cache;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        private const string CACHE_PRODUCT_COLLECTION_KEY = "AllProducts";

        public ProductService(IProductRepository repository, ISaleItemRepository repositorySaleItem, IMapper mapper, IUow uow, ICacheRepository cachingService, IProductDapperRepository repositoryDapper)
        {
            _repository = repository;
            _repositoryDapper = repositoryDapper;
            _repositorySaleItem = repositorySaleItem;
            _cache = cachingService;
            _mapper = mapper;
            _uow = uow;
            
        }

        public async Task<IEnumerable<ICommandResult>> Handle()
        {

            var entity = await _cache.GetCollection<Product>(CACHE_PRODUCT_COLLECTION_KEY);

            if (entity is null || !entity.Any())
            {
                entity = await _repository.GetAllAsync();

                if (entity.Count() <= 0) AddNotification("Warning", "Nenhum registro encontrado");

                if (!IsValid()) return default;

                await _cache.SetCollection(CACHE_PRODUCT_COLLECTION_KEY, entity);

                return _mapper.Map<IEnumerable<ProductResponse>>(entity.OrderBy(x => x.Description));
            }

            return _mapper.Map<IEnumerable<ProductResponse>>(entity.OrderBy(x => x.Description));
        }

        #region 'Dapper Implementation'

        public async Task<IEnumerable<ICommandResult>> HandleDapper()
        {
            var entity = await _cache.GetCollection<Product>(CACHE_PRODUCT_COLLECTION_KEY);

            if (entity is null || !entity.Any())
            {
                entity = await _repositoryDapper.GetAllAsync();

                if (entity.Count() <= 0) AddNotification("Warning", "Nenhum registro encontrado");

                if (!IsValid()) return default;

                await _cache.SetCollection(CACHE_PRODUCT_COLLECTION_KEY, entity);

                return _mapper.Map<IEnumerable<ProductResponse>>(entity.OrderBy(x => x.Description));
            }

            return _mapper.Map<IEnumerable<ProductResponse>>(entity.OrderBy(x => x.Description));
        }

        #endregion

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _cache.GetValue<Product>(id);

            if (entity is null)
            {
                entity = await _repository.GetDataAsync(x => x.Id == id);

                if (entity is null) AddNotification("Warning", "Produto não encontrado");

                if (!IsValid()) return default;

                await _cache.SetValue(id, entity);

                return _mapper.Map<ProductResponse>(entity);
            }

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ICommandResult> Handle(string barcode)
        {
            var entity = await _cache.GetValue<Product>(barcode);

            if (entity is null)
            {
                entity = await _repository.GetDataAsync(x => x.BarCode == barcode);

                if (entity is null) AddNotification("Warning", "Produto não encontrado");

                if (!IsValid()) return default;

                await _cache.SetValue(barcode, entity);

                return _mapper.Map<ProductResponse>(entity);
            }

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ICommandResult> Handle(ProductRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Product(default, request.BarCode, request.Description, request.Price);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            await _cache.SetValue(entity.Id, entity);
            await _cache.SetValue(entity.BarCode, entity);

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ICommandResult> Handle(ProductUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Product(request.Id, request.BarCode, request.Description, request.Price);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            await _cache.SetValue(entity.Id, entity);
            await _cache.SetValue(entity.BarCode, entity);

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var product = await _repositorySaleItem.GetDataAsync(x => x.ProductId == id);

            if (product is not null)
            {
                AddNotification("Warning", "Não é possível excluir esse produto, pois o mesmo está sendo usado");
                return default;
            }

            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ProductResponse>(entity);
        }

        private async Task<Product> ValidateInsert(ProductRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.BarCode == request.BarCode || x.Description.ToLower() == request.Description.ToLower());
            if (entity is not null) AddNotification("Warning", "Já existe um produto cadastrado com esse código de barras ou descrição");

            return entity;
        }

        private async Task<Product> ValidateUpdate(ProductUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && (x.BarCode == request.BarCode || x.Description.ToLower() == request.Description.ToLower()));
            if (entity is not null) AddNotification("Warning", "Já existe um produto cadastrado com esse código de barras ou descrição");

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification("Warning", "Produto não encontrado");

            return entity;
        }

    }
}
