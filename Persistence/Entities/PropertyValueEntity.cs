
using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Entities
{
    internal class PropertyValueEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; } = string.Empty;

        public string? Value { get; set; }
        public long ProductId { get; set; }
        public long PropertyId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual ProductTypeProperty TypeProperty { get; set; } = null!;
    }
}
