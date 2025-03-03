namespace Domain.Constants
{
    public abstract class Roles
    {
        public const string Administrator = "Administrator";

        public const string Guest = "Guest";

        private static readonly IReadOnlyCollection<string> ListOfRoles = new List<string>()
        {
            Administrator, 
            Guest,
        };
        public static bool IsCorrectRole(string roleName) => ListOfRoles.Contains(roleName);
    }
}
