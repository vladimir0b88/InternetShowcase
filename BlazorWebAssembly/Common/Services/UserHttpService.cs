using FluentValidation;
using System.Net.Http.Json;
using Domain.Entities;
using Application.Models;
using ErrorOr;

namespace BlazorWebAssembly.Common
{
    public class UserHttpService(IHttpClientFactory httpClientFactory,
                                 IValidator<UserRegisterDto> registerValidator) : IUserService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/Users";

        public async Task<ErrorOr<List<User>>> GetAllAsync()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResultAsync<List<User>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<string>> LoginAsync(UserLoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync($"{_controllerUri}/Login", loginDto);

            var result = await HttpResponseHandler.GetResultAsync<string>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Created>> RegisterAsync(UserRegisterDto registerDto)
        {
            //var validationResult = await registerValidator.ValidateAsync(registerDto);

            //if (!validationResult.IsValid)
            //    return validationResult.GetGeneralError();

            var response = await httpClient.PostAsJsonAsync($"{_controllerUri}/Register", registerDto);

            var result = await HttpResponseHandler.GetResultAsync<Created>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
