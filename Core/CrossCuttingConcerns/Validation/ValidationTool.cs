using System.Collections;
using FluentValidation;
using FluentValidation.Results;

namespace Core.CrossCuttingConcerns.Validation;

public static class ValidationTool
{
    public static void Validate(IValidator validator, object entity)
    {
        var context = new ValidationContext<object>(entity);
        var result = validator.Validate(context);

        if (!result.IsValid)
        {
            var errorList = new List<string>();
            List<ValidationFailure> validationFailures=new();
            result.Errors.ForEach(x =>
            {
                errorList.Add(x.ErrorMessage.ToString());
                validationFailures.Add(x);
                Console.WriteLine(x.ErrorMessage.ToString());
            });

            throw new ValidationException(validationFailures);
        }
    }
}