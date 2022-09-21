using Domain.DTOs.Product;
using FluentValidation;

namespace Business.Validators.ProductValidators;

public class ProductCreateValidator:AbstractValidator<ProductCreateRequestDto>
{
    public ProductCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Desc).NotEmpty().MinimumLength(2);
    }
}