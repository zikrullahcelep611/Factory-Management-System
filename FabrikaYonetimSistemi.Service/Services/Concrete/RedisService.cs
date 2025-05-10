using StackExchange.Redis;

namespace FabrikaYonetimSistemi.Service.Services.Concrete;

public class RedisService
{
    private readonly ConnectionMultiplexer _redis;
    public IDatabase Db { get; }

    public RedisService(string redisConnectionString)
    {
        _redis = ConnectionMultiplexer.Connect(redisConnectionString);
        Db = _redis.GetDatabase();
    }

    public IDatabase GetDatabase() => Db;
}
