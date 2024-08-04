namespace MetaNet.Microservices.Infrastructure.Caching
{
    public interface ICacheRepository
    {
        Task<T> GetValue<T>(Guid id);
        Task<T> GetValue<T>(string param);

        Task<IEnumerable<T>> GetCollection<T>(string collectionKey);

        Task SetValue<T>(Guid id, T entity);
        Task SetValue<T>(string param, T entity);

        Task SetCollection<T>(string collectionKey, IEnumerable<T> collection);
    }
}
