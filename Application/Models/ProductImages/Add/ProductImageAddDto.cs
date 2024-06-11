using Microsoft.AspNetCore.Http;

namespace Application.Models
{
    public class ProductImageAddDto
    {
        public long ProductId {  get; set; }

        public IFormFile FormFile { get; set; } = null!;
    }
}
