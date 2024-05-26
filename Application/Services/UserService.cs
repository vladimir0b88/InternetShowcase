using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Users.Login;
using Application.Models.Users.Register;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    internal class UserService(IUserRepository repository,
                               IPasswordHashService passwordHashService,
                               IJwtService jwtService,
                               IValidator<UserRegisterDto> createValidator) : IUserService
    {
        public async Task<Result<string>> Login(UserLoginDto loginDto)
        {
            var userResult = await repository.GetByEmail(loginDto.Email);

            if (userResult is ErrorResult<User> errorResult)
                return new ErrorResult<string>(message: errorResult.Message,
                                               errors: errorResult.Errors);

            User user = userResult.Data;

            var isCorrectPassword = passwordHashService.Verify(loginDto.Password, user.PasswordHash);

            if (isCorrectPassword == false)
                return new ErrorResult<string>(message: "Неправильный пароль",
                                               errors: [ErrorList.AuthError]);

            var token = jwtService.GenerateToken(user);

            return new SuccessResult<string>(token);
        }

        public async Task<Result> Register(UserRegisterDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if(!validationResult.IsValid)
                return new ValidationErrorResult(message: "Не удалось зарегистрировать пользователя, данные не прошли валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            User user = new User()
            {
                UserName = createDto.UserName,
                Email = createDto.Email,
                PasswordHash = passwordHashService.Generate(createDto.Password),
            };

            var result = await repository.AddUser(user);

            return result;
        }
    }
}
