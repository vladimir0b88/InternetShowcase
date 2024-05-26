using Application.Models.PropertyValues.Update;
using FluentValidation;

namespace BlazorWebAssembly.Validators.PropertyValues
{
    public class PropertyValueUpdateDtoListClientValidator : AbstractValidator<PropertyValueUpdateDtoList>
    {
        public PropertyValueUpdateDtoListClientValidator(IValidator<PropertyValueUpdateDto> validator)
        {
            RuleFor(pv => pv.List).NotEmpty();

            RuleForEach(pv => pv.List).SetValidator(validator);
        }
    }
}
