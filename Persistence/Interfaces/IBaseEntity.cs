
namespace Persistence.Interfaces
{
    interface IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}
