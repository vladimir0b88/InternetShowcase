
using Domain.Entities;

namespace Application.Common
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
