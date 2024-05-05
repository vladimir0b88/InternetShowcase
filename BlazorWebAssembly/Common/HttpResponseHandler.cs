using Application.Common.Errors;
using Application.Common.Models;
using System.Net.Http.Json;
using System.Net;
using Application;

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
                case HttpStatusCode.OK or HttpStatusCode.Created:
                    result = await response.Content.ReadFromJsonAsync<SuccessResult>();
                    break;

                case HttpStatusCode.UnprocessableEntity:
                    result = await response.Content.ReadFromJsonAsync<ValidationErrorResult>();
                    break;

                case HttpStatusCode.NotFound:
                    result = await response.Content.ReadFromJsonAsync<NotFoundErrorResult>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorResult>();
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

                case HttpStatusCode.UnprocessableEntity:
                    result = await response.Content.ReadFromJsonAsync<ValidationErrorResult<T>>();
                    break;

                case HttpStatusCode.NotFound:
                    result = await response.Content.ReadFromJsonAsync<NotFoundErrorResult<T>>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorResult<T>>();
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
