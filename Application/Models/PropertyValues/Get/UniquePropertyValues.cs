using Domain.Entities;

namespace Application.Common
{
    public class UniquePropertyValues
    {
        public TypeProperty TypeProperty { get; set; } = null!;

        public List<string> Values { get; set; } = null!;
    }
}
