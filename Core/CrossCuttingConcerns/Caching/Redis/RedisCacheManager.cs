using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.CrossCuttingConcerns.Caching.Redis;

public class RedisCacheManager : ICacheService
{
    private readonly RedisServer _redisServer;

    public RedisCacheManager(RedisServer redisServer)
    {
        _redisServer = redisServer;
    }

    public T Get<T>(string key)
    {
        var obj = _redisServer.GetDb().StringGet(key);
        var result = JsonConvert.DeserializeObject<T>(obj);

        return result;
    }

    public object Get(string key)
    {
        var obj = _redisServer.GetDb().StringGet(key);
        var result = JsonConvert.SerializeObject(obj);

        return result;
    }

    public void Add(string key, object value, int duration)
    {
        _redisServer.GetDb().StringSet(key: key, JsonSerializer.Serialize(value));
    }

    public bool IsAdded(string key)
    {
        return _redisServer.GetDb().KeyExists(key);
    }

    public void Remove(string key)
    {
        var keys = _redisServer.GetKeys();
        _redisServer.GetDb().KeyDelete(key);
    }

    public void RemoveByPattern(string[] keyPatterns)
    {
        var keys = _redisServer.GetKeys().ToList();
        foreach (var pattern in keyPatterns)
        {
            var matchedKeys = keys.Where(x => x.ToString().Contains(pattern)).ToList();
            foreach (var matchedKey in matchedKeys)
            {
                _redisServer.GetDb().KeyDelete(matchedKey.ToString());
            }
        }
    }
}