using Application.Models.Products.Create;
using FluentValidation;

namespace BlazorWebAssembly.Validators.Products
{
    public class ProductCreateDtoClientValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoClientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(512);

            RuleFor(p => p.Cost).GreaterThan(0);
        }
    }
}
