using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Entities
{
    internal class ProductEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; } = string.Empty; 
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Cost { get; set; }

        public long? TypeId { get; set; }
        public virtual ProductType? Type { get; set; } = null!;

        public virtual IList<PropertyValue> PropertyValues { get; set; } = null!;

        public virtual IList<ProductImage> Images { get; set; } = null!;

    }
}
