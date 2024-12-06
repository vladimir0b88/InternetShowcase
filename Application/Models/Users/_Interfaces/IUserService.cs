using Application.Models;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IUserService
    {
        Task<ErrorOr<List<User>>> GetAllAsync();
        Task<ErrorOr<Created>> RegisterAsync(UserRegisterDto createDto);

        Task<ErrorOr<string>> LoginAsync(UserLoginDto loginDto);
    }
}
