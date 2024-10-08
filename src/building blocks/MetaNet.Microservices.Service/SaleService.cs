﻿using AutoMapper;
using Esterdigi.Api.Core.Commands;
using FluentValidator;
using MetaNet.Microservices.Core.Constant;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Enums;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Transactions;

namespace MetaNet.Microservices.Service
{
    public class SaleService: Notifiable,
                ICommandHandler<SaleRegisterRequest>,
                ICommandHandler<SaleUpdateRequest>
    {
        private readonly ISaleRepository _repository;
        private readonly IUserRepository _repositoryUser;
        private readonly ISaleItemRepository _repositorySaleItem;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public SaleService(ISaleRepository repository, ISaleItemRepository repositorySaleItem, IMapper mapper, IUow uow, IUserRepository repositoryUser)
        {
            _repository = repository;
            _repositorySaleItem = repositorySaleItem;
            _repositoryUser = repositoryUser;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IEnumerable<ICommandResult>> Handle()
        {
            var entity = await _repository.GetAllSalesAsync();

            if (entity.Count() <= 0) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            return _mapper.Map<IEnumerable<SaleResponse>>(entity.OrderByDescending(x => x.SaleDate));
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id, include => include.User);

            if (entity is null) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            return _mapper.Map<SaleResponse>(entity);
        }

        public async Task<ICommandResult> Handle(SaleRegisterRequest request)
        {
            var user = await _repositoryUser.GetDataAsync(x => x.Id == request.UserId);

            if (user is null) AddNotification(Constants.AlertTitle, Constants.UserNotFound);

            var entity = new Sale(default, DateTime.Now, 0, PaymentFormat.Undefined, SaleStatus.Open, request.UserId);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleResponse>(entity);
        }

        public async Task<ICommandResult> Handle(SaleUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            var saleItem = await _repositorySaleItem.GetListDataAsync(x => x.SaleId == request.Id);

            if (entity is null) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (saleItem.Count() <= 0) AddNotification(Constants.AlertTitle, Constants.SaleItensNotFound);

            entity.CloseSale(saleItem.Sum(x => x.Total));
            entity.SetPaymentSale(request.PaymentFormat);

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            entity.CancelSale();

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<SaleResponse>(entity);
        }

        public async Task<IEnumerable<ICommandResult>> DeleteOpen()
        {
            var entity = await _repository.GetListDataAsync(x => x.SaleStatus == SaleStatus.Open);

            if (entity.Count() <= 0) AddNotification(Constants.AlertTitle, Constants.OpenedSalesNotFound);

            if (!IsValid()) return default;

            foreach (var item in entity) _repository.Delete(item);

            await _uow.CommitAsync();

            return _mapper.Map< IEnumerable<SaleResponse>>(entity);
        }

    }
}
