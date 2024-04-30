using Application.Models.Products.Update;
using FluentValidation;

namespace BlazorWebAssembly.Validators.Products
{
    public class ProductUpdateDtoClientValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoClientValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);
        }
    }
}
