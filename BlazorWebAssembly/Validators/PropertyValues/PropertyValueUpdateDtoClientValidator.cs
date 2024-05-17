using Application.Models.PropertyValues.Update;
using FluentValidation;

namespace BlazorWebAssembly.Validators.PropertyValues
{
    public class PropertyValueUpdateDtoClientValidator : AbstractValidator<PropertyValueUpdateDto>
    {
        public PropertyValueUpdateDtoClientValidator()
        {
            RuleFor(pv => pv.Id).NotEmpty();

            RuleFor(pv => pv.Value).MaximumLength(30);
        }
    }
}
