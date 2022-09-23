namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheService
{
    T Get<T>(string key);
    Object Get (string key);
    void Add (string key, object value, int duration);
    bool IsAdded (string key);
    void Remove(string key);
    void RemoveByPattern(string[] keyPattern);
}