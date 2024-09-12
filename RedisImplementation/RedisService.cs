using StackExchange.Redis;

namespace RedisImplementation
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(IConfiguration configuration)
        {
            // Get the Redis connection string from appsettings.json
            var redisConnectionString = configuration.GetSection("Redis").GetValue<string>("ConnectionString");
            _redis = ConnectionMultiplexer.Connect(redisConnectionString);
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }
    }
}
