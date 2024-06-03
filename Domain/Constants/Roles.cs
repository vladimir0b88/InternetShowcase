namespace Domain.Constants
{
    public abstract class Roles
    {
        public const string Administrator = nameof(Administrator);

        public const string Guest = nameof(Guest);

        private static readonly IReadOnlyCollection<string> ListOfRoles = new List<string>()
        {
            Administrator, 
            Guest,
        };
        public static bool IsCorrectRole(string roleName) => ListOfRoles.Contains(roleName);
    }
}
