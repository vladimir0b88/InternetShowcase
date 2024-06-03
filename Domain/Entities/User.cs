﻿
namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role {  get; set; } = string.Empty;
    }
}
