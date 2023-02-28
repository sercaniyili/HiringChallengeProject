using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using HiringChallange.Application.Interfaces.Cache;

namespace HiringChallange.Infrastructure.Services.Cache
{
    public class RedisDistrubutedCacheService : IRedisDistrubutedCache
    {
        private readonly IDistributedCache _distributedCache;
        public RedisDistrubutedCacheService(IDistributedCache distributedCache) => _distributedCache = distributedCache;

        public async Task<T> GetObjectAsync<T>(string key)
        {
            string serializedValues = String.Empty;
            var values = new List<T>();

            var cachedValue = await _distributedCache.GetAsync(key);

            if (cachedValue != null)
                serializedValues = Encoding.UTF8.GetString(cachedValue);

            return JsonSerializer.Deserialize<T>(serializedValues);
        }


        public async Task SetObjectAsync<T>(string key, T value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 30)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond)
            };

            var serilazedValue = JsonSerializer.Serialize(value);

            await _distributedCache.SetStringAsync(key, serilazedValue, distributedCacheEntryOptions);
        }
    }
}
