using Domain.DTOs.Product;
using FluentValidation;

namespace Business.Validators.ProductValidators;

public class ProductUpdateValidator:AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Desc).NotEmpty().MinimumLength(2);

    }
}