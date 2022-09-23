using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Validation;

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