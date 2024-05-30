
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<List<User>>> GetAll();
        Task<Result> AddUser(User user);

        Task<Result<User>> GetByEmail(string email);
    }
}
