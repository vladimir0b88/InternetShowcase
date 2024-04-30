using Application.Common.Errors;
using Application.Common.Models;
using System.Net.Http.Json;
using System.Net;

namespace BlazorWebAssembly.Common
{
    public static class HttpResponseHandler
    {
        public static async Task<Result> GetResult(HttpResponseMessage? response)
        {
            if (response is null)
                return new ErrorResult("Ответ от сервера не получен");

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
                    result = new ErrorResult("Непредусмотренный ответ от сервера");
                    break;
            }

            if (result is null)
                result = new ErrorResult("Пришел пустой json или ошибка при преобразовании json в объект");

            return result;
        }


        public static async Task<Result<T>> GetResult<T>(HttpResponseMessage? response)
        {
            if (response is null)
                return new ErrorResult<T>("Ответ от сервера не получен");

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
                    result = new ErrorResult<T>("Непредусмотренный ответ от сервера");
                    break;
            }

            if (result is null)
                result = new ErrorResult<T>("Пришел пустой json или ошибка при преобразовании json в объект");

            return result;
        }
    }
}
