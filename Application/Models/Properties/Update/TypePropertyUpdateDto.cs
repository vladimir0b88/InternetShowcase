
using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class TypePropertyUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }


    public static class TypePropertyUpdateMapper
    {
        public static ProductTypeProperty ToEntity(this TypePropertyUpdateDto dto)
        {
            return new ProductTypeProperty
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }

    public class TypePropertyUpdateDtoValidator : AbstractValidator<TypePropertyUpdateDto>
    {
        public TypePropertyUpdateDtoValidator()
        {
            RuleFor(tp => tp.Name).NotEmpty();

            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(64);
        }
    }
}
