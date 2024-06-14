using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorWebAssembly.Common
{
    public class CookieHandler(ILocalStorageService storageService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? token = await storageService.GetItemAsStringAsync("jwt-token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
