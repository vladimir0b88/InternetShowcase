using FluentValidation;

namespace Application.Models
{
    public class ProductTypeAddDtoValidator : AbstractValidator<ProductTypeAddDto>
    {
        public ProductTypeAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
