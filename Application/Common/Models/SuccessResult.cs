
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common
{
    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }

        [JsonConstructor]
        public SuccessResult(bool success)
        {
            Success = success;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data)
        {
            Success = true;
        }


        [JsonConstructor]
        public SuccessResult(bool success, T data) : base(data)
        {
            Success = success;
        }
    }
}
