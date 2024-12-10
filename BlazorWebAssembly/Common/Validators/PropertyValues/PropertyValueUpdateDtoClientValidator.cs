using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
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
