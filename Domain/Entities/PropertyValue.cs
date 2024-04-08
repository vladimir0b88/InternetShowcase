
namespace Domain.Entities
{
    public class PropertyValue
    {
        public long Id {  get; set; }
        public string? Value {  get; set; }
        public long ProductId {  get; set; }
        public long PropertyId {  get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual TypeProperty TypeProperty { get; set; } = null!;
    }
}
