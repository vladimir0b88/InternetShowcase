using Application.Models.ProductTypes.Create;
using FluentValidation;

namespace BlazorWebAssembly.Validators.ProductTypes
{
    public class ProductTypeCreateDtoClientValidator : AbstractValidator<ProductTypeCreateDto>
    {
        public ProductTypeCreateDtoClientValidator()
        {
            RuleFor(pt => pt.Name).NotEmpty().MaximumLength(64);
        }
    }
}
