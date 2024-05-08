using Application.Models.ProductTypes.Update;
using FluentValidation;

namespace BlazorWebAssembly.Validators.ProductTypes
{
    public class ProductTypeUpdateDtoClientValidator : AbstractValidator<ProductTypeUpdateDto>
    {
        public ProductTypeUpdateDtoClientValidator()
        {
            RuleFor(pt => pt.Id).NotEmpty();

            RuleFor(pt => pt.Name).NotEmpty().MaximumLength(64);
        }
    }
}
