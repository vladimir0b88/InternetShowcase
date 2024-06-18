
namespace Application.Models
{
    public class ProductImageAddDto
    {
        public long ProductId { get; set; }

        public string Format { get; set; } = string.Empty;
        public byte[] Image { get; set; } = null!;
    }
}
