
using Domain.Entities;
using FluentValidation;

namespace Application.Common
{
    public class ProductsFilter
    {
        public long? ProductTypeId { get; set; } = null;

        public int ItemsOnPage { get; set; } = 1;

        public int PageNumber { get; set; } = 1;

        public long? MinCost { get; set; }
        public long? MaxCost { get; set; }

        public List<PropertyFilter>? PropertyFilters { get; set; }

        public SortingMethods SortingMethod { get; set; } = SortingMethods.ByNameAsk;

        public enum SortingMethods
        {
            ByCostAsk,
            ByCostDesk,

            ByNameAsk,
            ByNameDesk,
        }

        public static IQueryable<Product> SortByMethod(IQueryable<Product> query, SortingMethods sortingMethod) => sortingMethod switch
        {
            SortingMethods.ByNameDesk => query.OrderByDescending(p => p.Name),
            SortingMethods.ByNameAsk => query.OrderBy(p => p.Name),
            SortingMethods.ByCostDesk => query.OrderByDescending(p => p.Cost),
            SortingMethods.ByCostAsk => query.OrderBy(p => p.Cost),
            _ => throw new NotImplementedException()
        };
    }

    public class ProductsFilterValidator : AbstractValidator<ProductsFilter>
    {
        public ProductsFilterValidator(IValidator<PropertyFilter> validator)
        {
            RuleFor(pf => pf.ItemsOnPage).NotEmpty()
                                         .GreaterThan(0)
                                         .LessThan(100);

            RuleFor(pf => pf.PageNumber).NotEmpty()
                                        .GreaterThan(0);

            When(pf => pf.MinCost is not null, () =>
            {
                RuleFor(pf => pf.MinCost).GreaterThanOrEqualTo(0).WithMessage("Минимальная стоимость должна быть больше 0")
                                         .LessThan(pf => pf.MaxCost).When(pf => pf.MaxCost is not null).WithMessage("Минимальная стоимость должна быть меньше максимальной стоимости");
            });

            When(pf => pf.MaxCost is not null, () =>
            {
                RuleFor(pf => pf.MaxCost).GreaterThanOrEqualTo(0).WithMessage("Максимальная стоимость должна быть больше нуля");
            });

            RuleForEach(pf => pf.PropertyFilters).SetValidator(validator);
        }
    }
}
