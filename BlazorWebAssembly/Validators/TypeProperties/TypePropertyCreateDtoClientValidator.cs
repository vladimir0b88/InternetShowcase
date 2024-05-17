using Application.Models.TypeProperties.Create;
using FluentValidation;

namespace BlazorWebAssembly.Validators.TypeProperties
{
    public class TypePropertyCreateDtoClientValidator : AbstractValidator<TypePropertyCreateDto>
    {
        public TypePropertyCreateDtoClientValidator()
        {
            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(30);
        }
    }
}
