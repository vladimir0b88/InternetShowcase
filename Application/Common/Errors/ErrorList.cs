using Application.Common.Models;

namespace Application
{
    public class ErrorList
    {
        public static readonly Error NotFound = new Error(
            code: nameof(NotFound),
            description: "Объект не был найден");

        public static readonly Error IsNull = new Error(
            code: nameof(IsNull),
            description: "Объект не может быть пустым");

        public static readonly Error FailedValidation = new Error(
            code: nameof(FailedValidation),
            description: "Объект не прошел валидацию");

        public static readonly Error ServerUnavailable = new Error(
            code: nameof(ServerUnavailable),
            description: "Ответ от сервера не получен");
    }
}
