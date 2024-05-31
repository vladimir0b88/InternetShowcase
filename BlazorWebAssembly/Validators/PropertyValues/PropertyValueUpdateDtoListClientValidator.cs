using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Validators
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
