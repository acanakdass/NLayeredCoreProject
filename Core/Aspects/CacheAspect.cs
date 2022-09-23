using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Aspects.Validation;

public class CacheAspect<TResponse> : IActionFilter where TResponse:class
{
    private readonly ICacheService _cacheService;
    private readonly int _duration;
    public CacheAspect( int duration = 30)
    {
        _duration = duration;
        _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Items.Add("actionArguments", context.ActionArguments);
        var key = GetActionKeyFromContext(context);
        var isAddedToCache = _cacheService.IsAdded(key.ToString());
        if (isAddedToCache)
        {
            var cachedResult = _cacheService.Get<TResponse>(key);
            context.Result = new OkObjectResult(cachedResult);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var key = GetActionKeyFromContext(context);
        var isAddedToCache = _cacheService.IsAdded(key);
        var result = context.Result as ObjectResult;
        if (!isAddedToCache)
        {
            _cacheService.Add(key, result?.Value, _duration);
        }
    }

    private string GetActionKeyFromContext(FilterContext context)
    {
        var args = context.HttpContext.Items.Where(x => x.Key == "actionArguments");
        var key = new StringBuilder();
        var actionName =context.RouteData.Values["action"].ToString();
        var controllerName =context.RouteData.Values["controller"].ToString();
        key.Append($"{controllerName}.{actionName},args:");
        foreach (var arg in args)
        {
            var argument = JsonSerializer.Serialize(arg.Value);
            key.Append(argument);
        }

        return key.ToString();
    }
}