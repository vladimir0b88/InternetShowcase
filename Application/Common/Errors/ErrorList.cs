﻿using Application.Common.Errors;

namespace Application.Common
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

        public static readonly Error AuthError = new Error(
            code: nameof(AuthError),
            description: "Ошибка аутентификации");

        public static readonly Error EntityAlreadyExist = new Error(
            code: nameof(EntityAlreadyExist),
            description: "Сущность уже существует");
    }
}
