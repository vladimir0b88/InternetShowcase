using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class ProductImageAddDto
    {
        public long ProductId { get; set; }

        public string Format { get; set; } = string.Empty;
        public byte[] Image { get; set; } = null!;
    }


    public static class ProductImageAddMapper
    {
        public static ProductImage ToEntity(this ProductImageAddDto dto)
        {
            return new ProductImage
            {
                ProductId = dto.ProductId,
                Format = dto.Format,
                Image = dto.Image,
            };
        }
    }


    public class ProductImageAddDtoValidator : AbstractValidator<ProductImageAddDto>
    {
        public ProductImageAddDtoValidator()
        {
            RuleFor(pi => pi.ProductId).NotEmpty();

            RuleFor(pi => pi.Format).NotEmpty();

        }
    }
}
