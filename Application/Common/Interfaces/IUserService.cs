using Application.Common.Models;
using Application.Models.Users.Login;
using Application.Models.Users.Register;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<Result> Register(UserRegisterDto createDto);

        Task<Result<string>> Login(UserLoginDto loginDto);
    }
}
