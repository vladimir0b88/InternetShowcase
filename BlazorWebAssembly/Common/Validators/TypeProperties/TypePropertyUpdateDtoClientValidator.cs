using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
{
    public class TypePropertyUpdateDtoClientValidator : AbstractValidator<TypePropertyUpdateDto>
    {
        public TypePropertyUpdateDtoClientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30);
        }
    }
}
