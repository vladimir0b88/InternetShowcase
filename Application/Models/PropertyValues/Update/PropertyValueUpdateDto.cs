
using Domain.Entities;

namespace Application.Models
{
    public class PropertyValueUpdateDto
    {
        public long Id {  get; set; }
        public string? Value { get; set; }
    }

    public static class PropertyValueUpdateMapper
    {
        public static PropertyValue ToEntity(this PropertyValueUpdateDto dto)
        {
            return new PropertyValue
            {
                Id = dto.Id,
                Value = dto.Value,
            };
        }
    }
}
