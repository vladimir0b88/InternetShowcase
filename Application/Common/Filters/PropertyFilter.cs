using FluentValidation;

namespace Application.Common
{
    public class PropertyFilter
    {
        public long PropertyId { get; set; }

        public List<string> Values { get; set; } = new();
    }

    public class PropertyFilterValidator : AbstractValidator<PropertyFilter>
    {
        public PropertyFilterValidator()
        {
            RuleFor(pf => pf.PropertyId).NotEmpty().GreaterThan(0);

            RuleFor(pf => pf.Values).NotEmpty()
                                    .Must(list => list.Count > 0).WithMessage("Для фильтрации по свойству необходимо указывать хотя бы одно значение свойства");
        }
    }
}
