using Microsoft.IdentityModel.Tokens;
using SemanaAcademica.CrossCutting.Security.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SemanaAcademica.CrossCutting.Security.Service
{
    public class AccessTokenService
    {
        private readonly AccessTokenSettings _jwtSettings;

        public AccessTokenService(AccessTokenSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey
                ?? throw new InvalidOperationException("SecretKey não configurada."));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}