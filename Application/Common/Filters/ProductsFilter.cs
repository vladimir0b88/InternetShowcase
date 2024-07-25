
using FluentValidation;

namespace Application.Common
{
    public class ProductsFilter
    {
        public long? ProductTypeId { get; set; } = null;

        public int ItemsOnPage { get; set; } = 1;

        public int PageNumber { get; set; } = 1;

        public long MinCost { get; set; } = 0;
        public long MaxCost { get; set; } = long.MaxValue;

        public List<PropertyFilter>? PropertyFilters { get; set; }

        public SortingMethods SortingMethod { get; set; } = SortingMethods.ByNameAsk;

        public enum SortingMethods
        {
            ByCostAsk,
            ByCostDesk,

            ByNameAsk,
            ByNameDesk,
        }
    }

    public class ProductsFilterValidator : AbstractValidator<ProductsFilter>
    {
        public ProductsFilterValidator(IValidator<PropertyFilter> validator)
        {
            RuleFor(pf => pf.ItemsOnPage).NotEmpty().GreaterThan(0);

            RuleFor(pf => pf.PageNumber).NotEmpty().GreaterThan(0);

            RuleFor(pf => pf.MinCost).NotEmpty()
                                     .GreaterThan(0)
                                     .LessThan(pf => pf.MaxCost);

            RuleFor(pf => pf.PropertyFilters).SetValidator(validator);
        }
    }
}
