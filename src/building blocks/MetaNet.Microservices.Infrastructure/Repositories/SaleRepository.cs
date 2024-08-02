using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Contexts;
using MetaNet.Microservices.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MetaNet.Microservices.Infrastructure.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(MetaNetDataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .Include(x => x.SaleItems)
                    .ThenInclude(x => x.Product)
                .ToListAsync();
        }
    }
}
