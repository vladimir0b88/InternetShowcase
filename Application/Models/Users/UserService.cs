using Domain.Constants;
using Domain.Entities;
using ErrorOr;
using FluentValidation;

namespace Application.Models
{
    internal class UserService(IUserRepository repository,
                               IPasswordHashService passHashService,
                               IJwtService jwtService,
                               IValidator<UserRegisterDto> createValidator) : IUserService
    {
        public async Task<ErrorOr<List<User>>> GetAllUsers()
        {
            var result = await repository.GetAll();

            return result;
        }

        public async Task<ErrorOr<string>> Login(UserLoginDto loginDto)
        {
            var searchResult = await repository.GetByEmail(loginDto.Email);

            if (searchResult.IsError)
                return searchResult.Errors;

            User user = searchResult.Value;

            var isCorrectPass = passHashService.Verify(loginDto.Password, user.PasswordHash);

            if (isCorrectPass == false)
                return Error.Failure(description: "Неправильный пароль");

            var token = jwtService.GenerateToken(user);

            return token;
        }

        public async Task<ErrorOr<Created>> Register(UserRegisterDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if(!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


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
