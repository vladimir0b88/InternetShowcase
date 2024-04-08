
namespace Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Cost { get; set; }

        public long? TypeId { get; set; }
        public virtual ProductType? Type { get; set; } = null!;

        public virtual IList<PropertyValue> PropertyValues { get; set; } = null!;
    }
}
