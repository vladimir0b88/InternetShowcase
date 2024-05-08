using Application.Models.TypeProperties.Update;
using FluentValidation;

namespace BlazorWebAssembly.Validators.TypeProperties
{
    public class TypePropertyUpdateDtoClientValidator : AbstractValidator<TypePropertyUpdateDto>
    {
        public TypePropertyUpdateDtoClientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
        }
    }
}
