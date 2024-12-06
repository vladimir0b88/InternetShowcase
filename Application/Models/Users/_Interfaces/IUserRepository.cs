using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IUserRepository
    {
        Task<ErrorOr<List<User>>> GetAllAsync();
        Task<ErrorOr<Created>> AddUserAsync(User user);

        Task<ErrorOr<User>> GetByEmailAsync(string email);
    }
}
