using Application.Common.Models;
using Application.Models.Users.Login;
using Application.Models.Users.Register;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<Result<List<User>>> GetAllUsers();
        Task<Result> Register(UserRegisterDto createDto);

        Task<Result<string>> Login(UserLoginDto loginDto);
    }
}
