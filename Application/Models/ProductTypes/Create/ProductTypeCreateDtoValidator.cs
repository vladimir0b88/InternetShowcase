using FluentValidation;

namespace Application.Models
{
    public class ProductTypeCreateDtoValidator : AbstractValidator<ProductTypeCreateDto>
    {
        public ProductTypeCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
