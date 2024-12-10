using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
{
    public class ProductTypeCreateDtoClientValidator : AbstractValidator<ProductTypeAddDto>
    {
        public ProductTypeCreateDtoClientValidator()
        {
            RuleFor(pt => pt.Name).NotEmpty().MaximumLength(30);
        }
    }
}
