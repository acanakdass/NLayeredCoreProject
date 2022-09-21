
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Aspects.Validation;

public class ValidationAspect : ActionFilterAttribute
{
    private Type _validatorType;

    public ValidationAspect(Type validatorType)
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new Exception("Not a validation class");
        }
        _validatorType = validatorType;
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var validator = (IValidator) Activator.CreateInstance(_validatorType);
        var entityType = _validatorType.BaseType.GetGenericArguments()[0];
        var entities = context.ActionArguments.Where(t => t.GetType() == entityType).ToList();
        var entType = context.ActionArguments.FirstOrDefault().GetType();
        foreach (var entity in entities)
        {
            ValidationTool.Validate(validator, entity);
        }
    }
}