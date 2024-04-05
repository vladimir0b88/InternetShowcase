
namespace Domain.Entities
{
    public class TypeProperty
    {
        public long Id { get; set; }

        public long? TypeId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ProductType? Type { get; set; } = null!;
    }
}
