using Application.Models;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IUserService
    {
        Task<ErrorOr<List<User>>> GetAllUsers();
        Task<ErrorOr<Created>> Register(UserRegisterDto createDto);

        Task<ErrorOr<string>> Login(UserLoginDto loginDto);
    }
}
