using FluentValidation;

namespace Application.Models.ProductTypes.Create
{
    public class ProductTypeCreateDtoValidator : AbstractValidator<ProductTypeCreateDto>
    {
        public ProductTypeCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
