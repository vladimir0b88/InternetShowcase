using ErrorOr;
using FluentValidation.Results;

namespace Application.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<Error> GetGeneralError(this ValidationResult result)
        {
            return result.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));
        }
    }
}
