using System.Net;
using System.Net.Http.Json;
using ErrorOr;

namespace BlazorWebAssembly.Common
{
    public static class HttpResponseHandler
    {
        public static async Task<ErrorOr<T>> GetResultAsync<T>(HttpResponseMessage? response)
        {
            if (response is null)
                return Error.Unexpected(description: "Ответ от сервера не получен");

            ErrorOr<T> result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result = await response.Content.ReadFromJsonAsync<T>();
                    break;

                case HttpStatusCode.NotFound:
                    result = await response.Content.ReadFromJsonAsync<ErrorOr<T>>();
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    result = await response.Content.ReadFromJsonAsync<ErrorOr<T>>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorOr<T>>();
                    break;

                case HttpStatusCode.Forbidden:
                    result = Error.Forbidden(description: "Недостаточно прав для выполнения данной операции");
                    break;

                case HttpStatusCode.Unauthorized:
                    result = Error.Unauthorized(description: "Пользователь не авторизирован");
                    break;

                default:
                    result = Error.Failure(description: "Непредусмотренный ответ от сервера");
                    break;
            }

            return result;
        }
    }
}
