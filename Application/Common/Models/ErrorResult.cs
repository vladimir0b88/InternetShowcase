
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;

namespace Application.Common
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; }
        public IReadOnlyCollection<Error> Errors { get; }

        [JsonConstructor]
        public ErrorResult(string message, bool success, IReadOnlyCollection<Error> errors)
        {
            Message = message;
            Success = success;
            Errors = errors ?? Array.Empty<Error>();
        }
    }

    public class ErrorResult<T> : Result<T>
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors) : base(default!)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; set; }
        public IReadOnlyCollection<Error> Errors { get; }

        [JsonConstructor]
        public ErrorResult(string message, bool success, IReadOnlyCollection<Error> errors, T data) : base(default!)
        {
            Message = message;
            Success = success;
            Errors = errors ?? Array.Empty<Error>();
            Data = data;
        }
    }
}
