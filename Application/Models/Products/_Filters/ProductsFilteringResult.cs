using Application.Common;
using Domain.Entities;

namespace Application.Models
{
    public class ProductsFilteringResult : FilteringResult<Product>
    {
        public ProductsSortingMethods SortingMethod { get; set; }

        public enum ProductsSortingMethods
        {
            ByCostAsk,
            ByCostDesk,

            ByNameAsk,
            ByNameDesk,
        }
    }
}
