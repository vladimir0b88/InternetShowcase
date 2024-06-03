using Application.Common;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorWebAssembly.Common
{
    public class CustomAuthStateProvider(ILocalStorageService storageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        private const string _localStorageKey = "auth";
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = await storageService.GetItemAsStringAsync(_localStorageKey);

            if (string.IsNullOrEmpty(token))
                return await AuthenticateAnonymous();


            var result = GetClaims(token);

            if (result is ErrorResult<CustomUserClaims>)
                return await AuthenticateAnonymous();

            var claims = SetClaimPrincipal(result.Data);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));

            return new AuthenticationState(claims);
        }

        private static Result<CustomUserClaims> GetClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new ErrorResult<CustomUserClaims>(message: "Передан пустой токен");

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            CustomUserClaims claims = new CustomUserClaims()
            {
                Id = (long)Convert.ToDouble(jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")!.Value),
                Username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value,
                Email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value,
                Role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value,
            };

            return new SuccessResult<CustomUserClaims>(claims);
        }

        private async Task<AuthenticationState> AuthenticateAnonymous()
        {
            AuthenticationState state = new AuthenticationState(_anonymous);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return await Task.FromResult(state);
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims userClaim)
        {
            if (userClaim.Email is null)
                return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim("userId", userClaim.Id.ToString()),
                    new Claim(ClaimTypes.Name, userClaim.Username),
                    new Claim(ClaimTypes.Email, userClaim.Email),
                    new Claim(ClaimTypes.Role, userClaim.Role),
                }, "JwtAuth"));
        }

        public async Task UpdateAuthState(string token)
        {
            var claims = new ClaimsPrincipal();

            if (!string.IsNullOrEmpty(token))
            {
                var result = GetClaims(token);

                if (result is ErrorResult<CustomUserClaims>)
                    return;

                claims = SetClaimPrincipal(result.Data);

                await storageService.SetItemAsStringAsync(_localStorageKey, token);
            }
            else
            {
                await storageService.RemoveItemAsync(_localStorageKey);
            }

            await storageService.SetItemAsStringAsync(_localStorageKey, token);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }
    }
}
