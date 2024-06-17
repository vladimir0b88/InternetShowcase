using FluentValidation;

namespace Application.Models
{
    public class ProductImageAddDtoValidator : AbstractValidator<ProductImageAddDto>
    {
        public ProductImageAddDtoValidator()
        {
            RuleFor(pi => pi.ProductId).NotEmpty();

            //RuleFor(pi => pi.FormFile).NotNull()
            //                          .Must(pi => pi.Length > 0)
            //                          .WithMessage("Изображение не может быть пустым");
        }
    }
}
