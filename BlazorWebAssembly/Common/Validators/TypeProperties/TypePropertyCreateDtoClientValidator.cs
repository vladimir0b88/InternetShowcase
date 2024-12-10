using Application.Models;
using FluentValidation;

namespace BlazorWebAssembly.Common
{
    public class TypePropertyCreateDtoClientValidator : AbstractValidator<TypePropertyAddDto>
    {
        public TypePropertyCreateDtoClientValidator()
        {
            RuleFor(tp => tp.Name).NotEmpty().MaximumLength(30);
        }
    }
}
