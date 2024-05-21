
using FluentValidation;

namespace Application.Models.Users.Register
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().MaximumLength(30);

            RuleFor(u => u.Email).NotEmpty().MaximumLength(60);

            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
