using AutoMapper;
using Esterdigi.Api.Core.Commands;
using FluentValidator;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Transactions;

namespace MetaNet.Microservices.Service
{
    public class SaleItemService: Notifiable,
                ICommandHandler<SaleItemRegisterRequest>,
                ICommandHandler<SaleItemUpdateRequest>
    {
        private readonly ISaleItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public SaleItemService(ISaleItemRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<IEnumerable<ICommandResult>> Handle()
        {
            var entity = await _repository.GetAllAsync();

            if (entity.Count() <= 0) AddNotification("Warning", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<IEnumerable<SaleItemResponse>>(entity);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Nenhum registro encontrado");

            if (!IsValid()) return default;

            return _mapper.Map<SaleItemResponse>(entity);
        }

        public async Task<ICommandResult> Handle(SaleItemRegisterRequest request)
        {
            var entity = new SaleItem(default, request.Quantity, request.Price, request.ProductId, request.SaleId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleItemResponse>(entity);
        }

        public async Task<ICommandResult> Handle(SaleItemUpdateRequest request)
        {
            var entity = new SaleItem(request.Id, request.Quantity, request.Price, request.ProductId, request.SaleId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleItemResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification("Warning", "Registro não encontrado");

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleItemResponse>(entity);
        }

    }
}
