
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
