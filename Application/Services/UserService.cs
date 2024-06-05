using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    internal class UserService(IUserRepository repository,
                               IPasswordHashService passHashService,
                               IJwtService jwtService,
                               IValidator<UserRegisterDto> createValidator) : IUserService
    {
        public async Task<Result<List<User>>> GetAllUsers()
        {
            var result = await repository.GetAll();

            return result;
        }

        public async Task<Result<string>> Login(UserLoginDto loginDto)
        {
            var searchResult = await repository.GetByEmail(loginDto.Email);

            if (searchResult is ErrorResult<User> errorResult)
                return new ErrorResult<string>(message: errorResult.Message,
                                               errors: errorResult.Errors);

            User user = searchResult.Data;

            var isCorrectPass = passHashService.Verify(loginDto.Password, user.PasswordHash);

            if (isCorrectPass == false)
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
                PasswordHash = passHashService.Generate(createDto.Password),
                Role = string.IsNullOrEmpty(createDto.Role) ? Roles.Guest : createDto.Role,
            };

            var result = await repository.AddUser(user);

            return result;
        }
    }
}
