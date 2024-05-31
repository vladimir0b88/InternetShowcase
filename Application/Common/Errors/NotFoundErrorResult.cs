using Application.Common;
using System.Text.Json.Serialization;

namespace Application.Common
{
    public class NotFoundErrorResult : ErrorResult
    {
        public NotFoundErrorResult(string message) : base(message)
        {
        }

        public NotFoundErrorResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }

        [JsonConstructor]
        public NotFoundErrorResult(string message, bool success, IReadOnlyCollection<Error> errors) : base(message, success, errors)
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

        [JsonConstructor]
        public NotFoundErrorResult(string message, bool success, IReadOnlyCollection<Error> errors, T data) : base(message, success, errors, data)
        {

        }
    }

}
