using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Validators
{
    public class ProductCreateDtoClientValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoClientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);
        }
    }
}
