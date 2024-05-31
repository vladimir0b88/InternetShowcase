using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Validators
{
    public class TypePropertyCreateDtoClientValidator : AbstractValidator<TypePropertyCreateDto>
    {
        public TypePropertyCreateDtoClientValidator()
        {
            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(30);
        }
    }
}
