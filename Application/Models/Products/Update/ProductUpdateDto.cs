using Domain.Entities;

namespace Application.Models
{
    public class ProductUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Cost { get; set; }
        public long? TypeId { get; set; } = null;

        public string Description { get; set; } = string.Empty;
    }


    public static class ProductUpdateMapper
    {
        public static Product ToEntity(this ProductUpdateDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Cost = dto.Cost,
                TypeId = dto.TypeId,
                Description = dto.Description,
            };
        }
    }
}
