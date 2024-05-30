using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorWebAssembly.Common
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState state;

            if(string.IsNullOrEmpty(Constant.JwtToken))
            {
                state = new AuthenticationState(_anonymous);

                NotifyAuthenticationStateChanged(Task.FromResult(state));

                return state;
            }

            //var getUserClaims = DecryptToken(Constant.JwtToken);

            //if(getUserClaims is null)
            //{
            //    state = new AuthenticationState(_anonymous);

            //    NotifyAuthenticationStateChanged(Task.FromResult(state));

            //    return state;
            //}

            //var claimsPrincipal = SetClaimPrincipal(getUserClaims);

            //state = new AuthenticationState(claimsPrincipal);

            //return state;

            return new AuthenticationState(_anonymous);
        }


        public static IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
