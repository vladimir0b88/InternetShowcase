using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
{
    public class ProductTypeUpdateDtoClientValidator : AbstractValidator<ProductTypeUpdateDto>
    {
        public ProductTypeUpdateDtoClientValidator()
        {
            RuleFor(pt => pt.Id).NotEmpty();

            RuleFor(pt => pt.Name).NotEmpty().MaximumLength(30);
        }
    }
}
