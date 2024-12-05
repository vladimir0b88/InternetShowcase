using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<ErrorOr<Created>> AddUser(User user)
        {
            if (user is null)
                return Error.Validation(description: "Нельзя добавить пустого пользователя");

            User? findUser = await context.Users.AsNoTracking()
                                                .FirstOrDefaultAsync(u => u.Email == user.Email);
            
            if(findUser is not null)
                return Error.NotFound(description: $"Пользователь с email: {user.Email} уже существует");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<List<User>>> GetAll()
        {
            List<User> users = await context.Users.AsNoTracking()
                                                  .ToListAsync();

            return users;
        }

        public async Task<ErrorOr<User>> GetByEmail(string email)
        {
            User? user = await context.Users.AsNoTracking()
                                            .FirstOrDefaultAsync(u => u.Email == email);

            if(user is null)
                return Error.NotFound(description: $"Пользователь c email: {email} не был найден");

            return user;
        }
    }
}
