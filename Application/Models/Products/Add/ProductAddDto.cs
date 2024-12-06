namespace Application.Models
{
    public class ProductAddDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Cost { get; set; }

        public long? TypeId { get; set; } = null;
    }
}
