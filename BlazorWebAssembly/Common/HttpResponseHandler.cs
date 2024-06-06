using Application.Common;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Common
{
    public static class HttpResponseHandler
    {
        public static async Task<Result> GetResult(HttpResponseMessage? response)
        {
            if (response is null)
                return new ErrorResult(message: "Ответ от сервера не получен",
                                       errors: [ErrorList.ServerUnavailable]);

            Result? result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result = await response.Content.ReadFromJsonAsync<SuccessResult>();
                    break;

                case HttpStatusCode.Created or HttpStatusCode.NoContent:
                    result = new SuccessResult();
                    break;

                case HttpStatusCode.NotFound:
                    result = await response.Content.ReadFromJsonAsync<NotFoundErrorResult>();
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    result = await response.Content.ReadFromJsonAsync<ValidationErrorResult>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorResult>();
                    break;

                case HttpStatusCode.Forbidden:
                    result = new ErrorResult(message: "Недостаточно прав для выполнения данной операции");
                    break;

                case HttpStatusCode.Unauthorized:
                    result = new ErrorResult(message: "Пользователь не авторизирован");
                    break;

                default:
                    result = new ErrorResult(message: "Непредусмотренный ответ от сервера");
                    break;
            }

            if (result is null)
                result = new ErrorResult(message: "Пришел пустой json или ошибка при преобразовании json в объект");

            return result;
        }


        public static async Task<Result<T>> GetResult<T>(HttpResponseMessage? response)
        {
            if (response is null)
                return new ErrorResult<T>(message: "Ответ от сервера не получен",
                                          errors: [ErrorList.ServerUnavailable]);

            Result<T>? result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result = await response.Content.ReadFromJsonAsync<SuccessResult<T>>();
                    break;

                case HttpStatusCode.NotFound:
                    result = await response.Content.ReadFromJsonAsync<NotFoundErrorResult<T>>();
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    result = await response.Content.ReadFromJsonAsync<ValidationErrorResult<T>>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorResult<T>>();
                    break;

                case HttpStatusCode.Forbidden:
                    result = new ErrorResult<T>(message: "Недостаточно прав для выполнения данной операции");
                    break;

                case HttpStatusCode.Unauthorized:
                    result = new ErrorResult<T>(message: "Пользователь не авторизирован");
                    break;

                default:
                    result = new ErrorResult<T>(message: "Непредусмотренный ответ от сервера");
                    break;
            }

            if (result is null)
                result = new ErrorResult<T>(message: "Пришел пустой json или ошибка при преобразовании json в объект");

            return result;
        }
    }
}
