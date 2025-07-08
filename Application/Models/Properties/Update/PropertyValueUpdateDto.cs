using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class PropertyValueUpdateDto
    {
        public long Id {  get; set; }
        public string? Value { get; set; }
    }

    public static class PropertyValueUpdateMapper
    {
        public static PropertyValue ToEntity(this PropertyValueUpdateDto dto)
        {
            return new PropertyValue
            {
                Id = dto.Id,
                Value = dto.Value,
            };
        }
    }


    public class PropertyValueUpdateDtoValidator : AbstractValidator<PropertyValueUpdateDto>
    {
        public PropertyValueUpdateDtoValidator()
        {
            RuleFor(pv => pv.Id).NotEmpty();

            RuleFor(pv => pv.Value).MaximumLength(64);
        }
    }
}
