
using Application.Common.Models;
using FluentValidation.Results;
using System.Text.Json.Serialization;

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

        [JsonConstructor]
        public ValidationErrorResult(string message, bool success, IReadOnlyCollection<Error> errors, IReadOnlyCollection<string> validationMessage) : base(message, success, errors)
        {
            ValidationMessage = validationMessage;
        }
    }

    public class ValidationErrorResult<T> : ErrorResult<T>
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

        [JsonConstructor]
        public ValidationErrorResult(string message, bool success, IReadOnlyCollection<Error> errors, IReadOnlyCollection<string> validationMessage, T data) : base(message, success, errors, data)
        {
            ValidationMessage = validationMessage;
        }
    }
}
