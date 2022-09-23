using AutoMapper;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
        var objType = _validatorType.BaseType.GetGenericArguments()[0];
        var objs = context.ActionArguments.Where(x => x.Value.GetType() == objType).Select(y => y.Value).ToList();
        foreach (var obj in objs)
        {
            ValidationTool.Validate(validator, obj);
        }
    }
}