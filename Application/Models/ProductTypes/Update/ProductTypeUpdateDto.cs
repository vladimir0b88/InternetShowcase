using Domain.Entities;

namespace Application.Models
{
    public class ProductTypeUpdateDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }


    public static class ProductTypeUpdateMapper
    {
        public static ProductType ToEntity(this ProductTypeUpdateDto dto)
        {
            return new ProductType
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
