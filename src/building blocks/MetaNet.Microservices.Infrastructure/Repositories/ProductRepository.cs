using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Contexts;
using MetaNet.Microservices.Infrastructure.Repositories.Base;

namespace MetaNet.Microservices.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MetaNetDataContext context) : base(context)
        {
        }

    }
}
