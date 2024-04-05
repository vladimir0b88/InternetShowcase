using Application.Common.Models;

namespace Application.Common.Errors
{
    public class NotFoundErrorResult : ErrorResult
    {
        public NotFoundErrorResult(string message) : base(message)
        {
        }

        public NotFoundErrorResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }

    public class NotFoundErrorResult<T> : ErrorResult<T>
    {
        public NotFoundErrorResult(string message) : base(message)
        {
        }

        public NotFoundErrorResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }

}
