using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
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
