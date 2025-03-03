
namespace Application.Common
{
    public class FilteringResult<T>
    {
        public List<T> Result { get; set; } = new();
        public long ItemsOnPage { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }

        public long TotalItems { get; set; }
    }
}
