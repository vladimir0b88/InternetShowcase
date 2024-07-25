using static Application.Common.ProductsFilter;

namespace Application.Common
{
    public class FilteringResult<T>
    {
        public List<T> Results { get; set; } = new();
        public SortingMethods SortingMethod { get; set; }
        public long ItemsOnPage { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }

        public long TotalItems { get; set; }
    }
}
