using Domain.Entities;

namespace Application.Models.Products.Update
{
    public class ProductUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Cost { get; set; }
        public long? TypeId { get; set; } = null;
    }
}
