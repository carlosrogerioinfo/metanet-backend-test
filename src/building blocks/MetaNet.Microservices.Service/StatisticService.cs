using AutoMapper;
using Esterdigi.Api.Core.Commands;
using FluentValidator;
using MetaNet.Microservices.Domain.Enums;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Transactions;

namespace MetaNet.Microservices.Service
{
    public class StatisticService: Notifiable
    {
        private readonly ISaleRepository _repositorySale;
        private readonly IUserRepository _repositoryUser;
        private readonly IProductRepository _repositoryProduct;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public StatisticService(ISaleRepository repositorySale, IUserRepository repositoryUser, IProductRepository repositoryProduct, IMapper mapper, IUow uow)
        {
            _repositorySale = repositorySale;
            _repositoryUser = repositoryUser;
            _repositoryProduct = repositoryProduct;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ICommandResult> Handle()
        {
            var salesOpened = await _repositorySale.Count(x => x.SaleStatus == SaleStatus.Open);
            var salesClosed = await _repositorySale.Count(x => x.SaleStatus == SaleStatus.Closed);
            var salesCanceled = await _repositorySale.Count(x => x.SaleStatus == SaleStatus.Canceled);

            var users = await _repositoryUser.GetAllAsync();

            var products = await _repositoryProduct.GetAllAsync();

            return new StatisticResponse
            {
                CanceledSalesCount = salesCanceled,
                ClosedSalesCount = salesClosed,
                OpenSalesCount = salesOpened,
                ProductsCount = products.Count(),
                UsersCount = users.Count()
            };
        }

    }
}
