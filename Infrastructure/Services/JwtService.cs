using Application.Common;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    internal class JwtService(IOptions<JwtOptions> options) : IJwtService
    {
        private readonly JwtOptions _options = options.Value;
        public string GenerateToken(User user)
        {
            Claim[] claims = [
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)];

            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                                                               SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _options.Issuer,
                                             audience: _options.Audience,    
                                             claims: claims,
                                             signingCredentials: signingCredential,
                                             expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
