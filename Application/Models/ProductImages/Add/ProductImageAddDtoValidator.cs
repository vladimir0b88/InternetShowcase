using FluentValidation;

namespace Application.Models
{
    public class ProductImageAddDtoValidator : AbstractValidator<ProductImageAddDto>
    {
        public ProductImageAddDtoValidator()
        {
            RuleFor(pi => pi.ProductId).NotEmpty();


        }
    }
}
