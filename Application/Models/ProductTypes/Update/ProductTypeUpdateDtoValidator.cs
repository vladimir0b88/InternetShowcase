using FluentValidation;

namespace Application.Models
{
    public class ProductTypeUpdateDtoValidator : AbstractValidator<ProductTypeUpdateDto>
    {
        public ProductTypeUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
