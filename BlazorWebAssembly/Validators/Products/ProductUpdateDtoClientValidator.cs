using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Validators
{
    public class ProductUpdateDtoClientValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoClientValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);
        }
    }
}
