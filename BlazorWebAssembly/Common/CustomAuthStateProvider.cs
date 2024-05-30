using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorWebAssembly.Common
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState state;

            if (string.IsNullOrEmpty(Constant.JwtToken))
                return await AuthenticateAnonymous();

            try
            {
                var getUserClaims = DecryptToken(Constant.JwtToken);

                if (getUserClaims == null)
                    return await AuthenticateAnonymous();

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);

                state = new AuthenticationState(claimsPrincipal);

                return state;
            }
            catch
            {
                return await AuthenticateAnonymous();
            }
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
                    new Claim("userName", userClaim.Username),
                    new Claim("userEmail", userClaim.Email),
                }, "JwtAuth"));
        }

        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(jwtToken);

            CustomUserClaims claims = new CustomUserClaims()
            {
                Id = (long)Convert.ToDouble(token.Claims.FirstOrDefault(c => c.ValueType == "userId")!.Value),
                Username = token.Claims.FirstOrDefault(c => c.ValueType == "userName")!.Value,
                Email = token.Claims.FirstOrDefault(c => c.ValueType == "userEmail")!.Value
            };

            return claims;
        }

        public async void UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            if(!string.IsNullOrEmpty(jwtToken))
            {
                Constant.JwtToken = jwtToken;

                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                Constant.JwtToken = null!;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
