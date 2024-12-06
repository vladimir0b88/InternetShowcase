using Application.Common;
using Domain.Constants;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    internal class UserService(IUserRepository userRepository,
                               IPasswordHashService passHashService,
                               IJwtService jwtService,
                               IValidator<UserRegisterDto> createValidator) : IUserService
    {
        public async Task<ErrorOr<List<User>>> GetAllAsync()
        {
            var result = await userRepository.GetAllAsync();

            return result;
        }

        public async Task<ErrorOr<string>> LoginAsync(UserLoginDto loginDto)
        {
            var searchResult = await userRepository.GetByEmailAsync(loginDto.Email);

            if (searchResult.IsError)
                return searchResult.Errors;

            User user = searchResult.Value;

            var isCorrectPass = passHashService.Verify(loginDto.Password, user.PasswordHash);

            if (isCorrectPass == false)
                return Error.Failure(description: "Неправильный пароль");

            var token = jwtService.GenerateToken(user);

            return token;
        }

        public async Task<ErrorOr<Created>> RegisterAsync(UserRegisterDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            User user = new User()
            {
                UserName = createDto.UserName,
                Email = createDto.Email,
                PasswordHash = passHashService.Generate(createDto.Password),
                Role = string.IsNullOrEmpty(createDto.Role) ? Roles.Guest : createDto.Role,
            };

            var result = await userRepository.AddUserAsync(user);

            return result;
        }
    }
}
