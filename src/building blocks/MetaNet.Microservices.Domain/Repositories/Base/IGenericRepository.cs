using System.Linq.Expressions;

namespace MetaNet.Microservices.Domain.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(int id);

        Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression);

        Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);

        Task<int> Count(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);


        Task UpdateAsync(T entity, bool modifySingleEntity = false);

        void Delete(T entity);
    }
}
