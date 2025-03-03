using Domain.Entities;

namespace Application.Models
{
    public class ProductAddDto
    {
        public string Name { get; set; } = string.Empty;

        public long Cost { get; set; }

        public long? TypeId { get; set; } = null;

        public string Description { get; set; } = string.Empty;
    }



    public static class ProductsAddMapper
    {
        public static Product ToEntity(this ProductAddDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Cost = dto.Cost,
                TypeId = dto.TypeId,
                Description = dto.Description,
            };
        }
    }
}
