using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories.Base;

namespace MetaNet.Microservices.Domain.Repositories
{

    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> LoginAsync(string email, string password);
    }
}
