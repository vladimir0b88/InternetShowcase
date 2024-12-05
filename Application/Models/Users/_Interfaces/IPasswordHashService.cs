namespace Application.Models
{
    public interface IPasswordHashService
    {
        public string Generate(string password);

        public bool Verify(string password, string hashedPassword);
    }
}
