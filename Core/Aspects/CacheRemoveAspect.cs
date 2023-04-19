using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects;

/// <summary>
/// Method <c>CacheRemoveAspect</c> asda
/// <code>this is code section</code>
/// <param name="patterns"> asdasd</param>
/// </summary>
public class CacheRemoveAspect:ActionFilterAttribute
{
    private readonly ICacheService _cacheService;
    private readonly string[] _keyPatterns;

    public CacheRemoveAspect(string patterns)
    {
        _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        _keyPatterns = patterns.Split(",");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _cacheService.RemoveByPattern(_keyPatterns);   
    }
}