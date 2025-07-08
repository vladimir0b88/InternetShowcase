using Domain.Entities;
using FluentValidation;

namespace Application.Models
{
    public class UserRegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    public static class UserRegisterMapper
    {
        public static User ToEntity(this UserRegisterDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
        }
    }


    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().MaximumLength(30);

            RuleFor(u => u.Email).NotEmpty().MaximumLength(60);

            RuleFor(u => u.Password).NotEmpty();

            //RuleFor(u => u.Role).Must(u => u is not null)
            //                    .When(u => Roles.IsCorrectRole(u.Role))
            //                    .WithMessage("Указана некорректная роль");
        }
    }
}
