using Microsoft.IdentityModel.Tokens;
using SemanaAcademica.CrossCutting.Security.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SemanaAcademica.CrossCutting.Security.Service
{
    public class AccessTokenService
    {
        private readonly AccessTokenSettings JwtSettings;

        public AccessTokenService(AccessTokenSettings jwtSettings)
        {
            this.JwtSettings = jwtSettings;
        }

        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JwtSettings.SecretKey ?? throw new NullReferenceException("SecretKey Nulo"));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
