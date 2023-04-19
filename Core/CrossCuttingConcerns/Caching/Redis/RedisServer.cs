using StackExchange.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis;

public class RedisServer
{
    private readonly string _host;
    private readonly int _port;
    private ConnectionMultiplexer _connectionMultiplexer;
    public RedisServer(RedisSettings redisSettings)
    {
        _host = redisSettings.Host;
        _port = redisSettings.Port;
    }

    public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect(($"{_host}:{_port}"));
    public IDatabase GetDb() => _connectionMultiplexer.GetDatabase();
    public IEnumerable<RedisKey> GetKeys() => _connectionMultiplexer.GetServer($"{_host}:{_port}").Keys();
}