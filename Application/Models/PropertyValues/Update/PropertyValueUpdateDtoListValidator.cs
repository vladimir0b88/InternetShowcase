using FluentValidation;

namespace Application.Models.PropertyValues.Update
{
    public class PropertyValueUpdateDtoListValidator : AbstractValidator<PropertyValueUpdateDtoList>
    {
        public PropertyValueUpdateDtoListValidator(IValidator<PropertyValueUpdateDto> validator)
        {
            RuleFor(pv => pv.List).NotEmpty();

            RuleForEach(pv => pv.List).SetValidator(validator);
        }
    }
}
