using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Repositories.Base;

namespace MetaNet.Microservices.Domain.Repositories
{

    public interface ISaleRepository: IGenericRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
    }
}
