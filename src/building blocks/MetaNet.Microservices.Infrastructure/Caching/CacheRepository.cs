using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MetaNet.Microservices.Infrastructure.Caching
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions{
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(120),
                SlidingExpiration = TimeSpan.FromMinutes(60)
            };
        }

        public async Task<T> GetValue<T>(Guid id)
        {
            var key = id.ToString().ToLower();

            var result = await _cache.GetStringAsync(key);

            if (result is null) return default;

            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<T> GetValue<T>(string param)
        {
            var key = param.ToString().ToLower();

            var result = await _cache.GetStringAsync(key);

            if (result is null) return default;

            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<IEnumerable<T>> GetCollection<T>(string collectionKey)
        {
            var result = await _cache.GetStringAsync(collectionKey);

            if (result is null) return default;

            return JsonSerializer.Deserialize<IEnumerable<T>>(result);
        }

        public async Task SetValue<T>(Guid id, T entity)
        {
            var key = id.ToString().ToLower();

            var newValue = JsonSerializer.Serialize(entity);

            await _cache.SetStringAsync(key, newValue);
        }

        public async Task SetValue<T>(string param, T entity)
        {
            var key = param.ToString().ToLower();

            var newValue = JsonSerializer.Serialize(entity);

            await _cache.SetStringAsync(key, newValue);
        }

        public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collection)
        {
            var newValue = JsonSerializer.Serialize(collection);

            await _cache.SetStringAsync(collectionKey, newValue);
        }

    }
}
