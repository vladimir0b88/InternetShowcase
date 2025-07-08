using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Entities
{
    class ProductTypeEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;

        public virtual IList<Product> Products { get; set; } = new List<Product>();

        public virtual IList<ProductTypeProperty> Properties { get; set; } = new List<ProductTypeProperty>();
    }
}
