using FluentValidation;

namespace Application.Models
{
    public class PropertyValueUpdateDtoValidator : AbstractValidator<PropertyValueUpdateDto>
    {
        public PropertyValueUpdateDtoValidator()
        {
            RuleFor(pv => pv.Id).NotEmpty();

            RuleFor(pv => pv.Value).MaximumLength(64);
        }
    }
}
