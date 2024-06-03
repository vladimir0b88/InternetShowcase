using Application.Models;
using Domain.Entities;

namespace Application.Common
{
    public interface IUserService
    {
        Task<Result<List<User>>> GetAllUsers();
        Task<Result> Register(UserRegisterDto createDto);

        Task<Result<string>> Login(UserLoginDto loginDto);
    }
}
