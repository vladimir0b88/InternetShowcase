
namespace Domain.Entities
{
    public class ProductType
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual IList<Product> Products { get; set; } = new List<Product>();

        public virtual IList<TypeProperty> Properties { get; set; } = new List<TypeProperty>();
    }
}
