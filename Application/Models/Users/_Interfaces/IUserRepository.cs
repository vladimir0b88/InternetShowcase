using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IUserRepository
    {
        Task<ErrorOr<List<User>>> GetAll();
        Task<ErrorOr<Created>> AddUser(User user);

        Task<ErrorOr<User>> GetByEmail(string email);
    }
}
