namespace BlazorWebAssembly.Common
{
    public class CustomUserClaims
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
