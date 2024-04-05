using Domain.Abstractions;

namespace Domain.Entities
{
    public class ProductType
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public IList<Product> Products { get; private set; } = new List<Product>();
    }
}
