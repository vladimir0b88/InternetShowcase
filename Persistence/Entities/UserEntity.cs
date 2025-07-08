using Persistence.Interfaces;

namespace Persistence.Entities
{
    class UserEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; } = string.Empty;


        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
