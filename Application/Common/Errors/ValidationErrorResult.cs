
using Application.Common.Models;
using FluentValidation.Results;

namespace Application.Common.Errors
{
    public class ValidationErrorResult : ErrorResult
    {
        public IReadOnlyCollection<string> ValidationMessage { get; }

        public ValidationErrorResult(string message, 
                                     IReadOnlyCollection<ValidationFailure> validationErrors) : base(message)
        {
            ValidationMessage = validationErrors.Select(e => e.ErrorMessage).ToList();
        }

        public ValidationErrorResult(string message,
                                     IReadOnlyCollection<Error> errors,
                                     IReadOnlyCollection<ValidationFailure> validationErrors) : base(message, errors)
        {
            ValidationMessage = validationErrors.Select(e => e.ErrorMessage).ToList();
        }
    }

    public class ValidationErrorResult<T> : ErrorResult<T>
    {
        public IReadOnlyCollection<string> ValidationErrorsMessage { get; }
        public ValidationErrorResult(string message, 
                                     IReadOnlyCollection<ValidationFailure> validationErrors) : base(message)
        {
            ValidationErrorsMessage = validationErrors.Select(e => e.ErrorMessage).ToList();
        }

        public ValidationErrorResult(string message,
                                     IReadOnlyCollection<Error> errors,
                                     IReadOnlyCollection<ValidationFailure> validationErrors) : base(message, errors)
        {
            ValidationErrorsMessage = validationErrors.Select(e => e.ErrorMessage).ToList();
        }
    }
}
