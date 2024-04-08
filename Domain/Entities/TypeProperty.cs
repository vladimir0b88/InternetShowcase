
namespace Domain.Entities
{
    public class TypeProperty
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public long? TypeId { get; set; }
        public virtual ProductType? Type { get; set; } = null!;

        public virtual IList<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
    }
}
