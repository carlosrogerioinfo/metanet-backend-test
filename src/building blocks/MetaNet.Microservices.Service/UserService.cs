using AutoMapper;
using Esterdigi.Api.Core.Commands;
using Esterdigi.Api.Core.Helpers.Encrypt;
using FluentValidator;
using MetaNet.Microservices.Core.Constant;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Transactions;

namespace MetaNet.Microservices.Service
{
    public class UserService: Notifiable,
                ICommandHandler<UserRegisterRequest>,
                ICommandHandler<UserUpdateRequest>
    {
        private readonly IUserRepository _repository;
        private readonly ISaleItemRepository _repositorySaleItem;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public UserService(IUserRepository repository, ISaleItemRepository repositorySaleItem, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _repositorySaleItem = repositorySaleItem;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<UserResponse> Handle(AuthenticationRequest request)
        {
            var entity = await _repository.LoginAsync(request.Email, Cryptography.EncryptPassword(request.Password));

            if (entity is null)
            {
                AddNotification(Constants.AlertTitle, Constants.InvalidUserOrPassword);
                return default;
            }

            if (!IsValid()) return default;

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<IEnumerable<ICommandResult>> Handle()
        {
            var entity = await _repository.GetAllAsync();

            if (entity.Count() <= 0) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            return _mapper.Map<IEnumerable<UserResponse>>(entity);
        }

        public async Task<ICommandResult> Handle(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(UserRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new User(default, request.Name, request.Email, request.UserType, Cryptography.EncryptPassword(request.Password));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Handle(UserUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new User(request.Id, request.Name, request.Email, request.UserType, Cryptography.EncryptPassword(request.Password));

            AddNotifications(entity.Notifications);

            if (!IsValid()) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var entity = await _repository.GetDataAsync(x => x.Id == id);

            if (entity is null) AddNotification(Constants.AlertTitle, Constants.RegisterNotFound);

            if (!IsValid()) return default;

            _repository.Delete(entity);
            await _uow.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }


        private async Task<User> ValidateInsert(UserRegisterRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification(Constants.AlertTitle, Constants.RegisterWithMailInserted);

            return entity;
        }

        private async Task<User> ValidateUpdate(UserUpdateRequest request)
        {
            var entity = await _repository.GetDataAsync(x => x.Id != request.Id && x.Email.ToLower() == request.Email.ToLower());
            if (entity is not null) AddNotification(Constants.AlertTitle, Constants.RegisterWithMailInserted);

            entity = await _repository.GetDataAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification(Constants.AlertTitle, Constants.UserNotFound);

            return entity;
        }

    }
}
