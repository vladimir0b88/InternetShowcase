using Domain.Entities;

namespace Application.Models
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
