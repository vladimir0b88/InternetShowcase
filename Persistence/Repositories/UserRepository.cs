using Application;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<Result> AddUser(User user)
        {
            if(user is null)
                return new ErrorResult(message: "Нельзя добавить пустого пользователя",
                                        errors: [ErrorList.IsNull]);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<User>> GetByEmail(string email)
        {
            User? user = await context.Users.AsNoTracking()
                                            .FirstOrDefaultAsync(u => u.Email == email);

            if(user is null)
                return new NotFoundErrorResult<User>(message: $"Пользователь c email: {email} не был найден",
                                                     errors: [ErrorList.NotFound]);

            return new SuccessResult<User>(user);
        }
    }
}
