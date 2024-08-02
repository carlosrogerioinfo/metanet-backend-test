using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Contexts;
using MetaNet.Microservices.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MetaNet.Microservices.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MetaNetDataContext context) : base(context)
        {

        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

    }
}
