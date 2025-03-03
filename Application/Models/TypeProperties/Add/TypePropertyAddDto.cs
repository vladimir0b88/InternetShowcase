
using Domain.Entities;

namespace Application.Models
{
    public class TypePropertyAddDto
    {
        public string Name { get; set; } = string.Empty;
        public long? TypeId { get; set; }
    }


    public static class TypePropertyAddMapper
    {
        public static TypeProperty ToEntity(this TypePropertyAddDto dto)
        {
            return new TypeProperty
            {
                Name = dto.Name,
                TypeId = dto.TypeId,
            };
        }

    }
}
