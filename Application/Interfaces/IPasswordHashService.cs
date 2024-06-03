namespace Application.Common
{
    public interface IPasswordHashService
    {
        public string Generate(string password);

        public bool Verify(string password, string hashedPassword);
    }
}
