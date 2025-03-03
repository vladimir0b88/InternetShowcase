using Domain.Entities;

namespace Application.Models
{
    public class ProductTypeAddDto
    {
        public string Name { get; set; } = string.Empty;
    }


    public static class ProductTypeAddMapper
    {
        public static ProductType ToEntity(this ProductTypeAddDto dto)
        {
            return new ProductType
            {
                Name = dto.Name,
            };
        }
    }

}
