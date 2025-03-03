
using Domain.Constants;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public class UserRegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    public static class UserRegisterMapper
    {
        public static User ToEntity(this UserRegisterDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
        }
    }
}
