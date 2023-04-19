namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheService
{
    
    /// <summary>
    /// Finds the record with passed key and casts to object type which passed as generic type
    /// </summary>
    /// <typeparam name="T">Type to cast the result</typeparam>
    /// <returns></returns>
    T Get<T>(string key);
    
    
    /// <summary>
    /// Adds record to redis as string
    /// </summary>
    /// <param name="key">Key of record</param>
    /// <param name="value">Object to cache</param>
    /// <param name="duration">Number of minutes it should take before record will be automatically deleted</param>
    void Add (string key, object value, int duration);
    
    
    /// <summary>
    /// Checks if any record with passed key exists
    /// </summary>
    /// <returns>True if record with key exists.</returns>
    bool IsAdded (string key);
    
    
    /// <summary>
    /// Removes the record with passed key from cache
    /// </summary>
    /// <param name="key">Key of record to remove</param>
    void Remove(string key);
    
    /// <summary>
    /// Finds and removes all records which contains any keyword passed as parameter
    /// </summary>
    /// <param name="keyPatterns">An array of string patterns keys that contain any of them.</param>
    void RemoveByPattern(string[] keyPatterns);
}