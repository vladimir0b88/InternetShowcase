using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class ProductTypeAddDto
    {
        public string Name { get; set; } = string.Empty;
    }


    public static class ProductTypeAddMapper
    {
        public static ProductType ToEntity(this ProductTypeAddDto dto)
        {
            return new ProductType
            {
                Name = dto.Name,
            };
        }
    }


    public class ProductTypeAddDtoValidator : AbstractValidator<ProductTypeAddDto>
    {
        public ProductTypeAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
