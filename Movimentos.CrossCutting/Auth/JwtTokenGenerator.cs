using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movimentos.CrossCutting.Auth.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movimentos.CrossCutting.Auth
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly AuthSettings _settings;

        public JwtTokenGenerator(IOptions<AuthSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}