using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Aspects;

/// <summary>
/// If there is a cached record for same method and args, gets the result from cache.
/// If not, caches returned result after action executed. 
/// </summary>
/// <typeparam name="TResponse">Type that method returns after a successful operation.</typeparam>
public class CacheAspect<TResponse> : IActionFilter where TResponse : class
{
    private readonly ICacheService _cacheService;
    private readonly int _duration;
    private string _cacheKey;

    public CacheAspect(ICacheService cacheService)
    {
        _cacheService = cacheService;
        _duration = 30;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        SetCacheKey(context);
        var isAddedToCache = _cacheService.IsAdded(_cacheKey);
        if (isAddedToCache)
            context.Result = new OkObjectResult(_cacheService.Get<TResponse>(_cacheKey));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = context.Result as ObjectResult;
        _cacheService.Add(_cacheKey, result?.Value, _duration);
    }

    /// <summary>
    /// Creates a key to cache record by combining action name, controller name and arguments as string and binds to "_cacheKey" variable
    /// </summary>
    /// <param name="context">Current ActionExecutingContext item</param>
    private void SetCacheKey(ActionExecutingContext context)
    {
        var key = new StringBuilder();
        var actionName = context.RouteData.Values["action"]?.ToString();
        var controllerName = context.RouteData.Values["controller"]?.ToString();
        key.Append($"{controllerName}.{actionName},args:");
        foreach (var arg in context.ActionArguments)
            key.Append(JsonSerializer.Serialize(arg.Value));
        _cacheKey = key.ToString();
    }
}