
using Domain.Entities;

namespace Application.Models
{
    public class TypePropertyUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }


    public static class TypePropertyUpdateMapper
    {
        public static TypeProperty ToEntity(this TypePropertyUpdateDto dto)
        {
            return new TypeProperty
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
