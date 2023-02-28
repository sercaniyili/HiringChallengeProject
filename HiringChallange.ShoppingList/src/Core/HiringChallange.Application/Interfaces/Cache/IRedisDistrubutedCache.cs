namespace HiringChallange.Application.Interfaces.Cache
{
    public interface IRedisDistrubutedCache
    {
        Task<T> GetObjectAsync<T>(string key);
        Task SetObjectAsync<T>(string key, T value, int absoluteExpirationMinute = 1,
            int slidingExpirationSecond = 10);
    }
}
