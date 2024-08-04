using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Repositories.Dapper.Base;
using System.Data;

namespace MetaNet.Microservices.Infrastructure.Repositories
{
    public class ProductDapperRepository : GenericDapperRepository<Product>, IProductDapperRepository
    {
        public ProductDapperRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

    }
}
