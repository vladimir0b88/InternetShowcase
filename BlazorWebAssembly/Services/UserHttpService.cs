﻿using FluentValidation;
using System.Net.Http.Json;
using BlazorWebAssembly.Common;
using Domain.Entities;
using Application.Models;
using Application.Common;

namespace BlazorWebAssembly.Services
{
    public class UserHttpService(IHttpClientFactory httpClientFactory,
                                 IValidator<UserRegisterDto> registerValidator) : IUserService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/Users";

        public async Task<Result<List<User>>> GetAllUsers()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResult<List<User>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result<string>> Login(UserLoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync($"{_controllerUri}/Login", loginDto);

            var result = await HttpResponseHandler.GetResult<string>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> Register(UserRegisterDto registerDto)
        {
            var validationResult = await registerValidator.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Регистрация не удалась из-за ошибок валидации",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync($"{_controllerUri}/Register", registerDto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
