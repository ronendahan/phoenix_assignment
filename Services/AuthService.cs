using GitHubRepositories.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GitHubRepositories.Services
{
    public interface IAuth {
        public Task<string> GenerateToken(string sessionId);
    }
    public class AuthService: IAuth
    {
        private readonly IConfiguration _appConfig;
        public AuthService(IConfiguration appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<string> GenerateToken(string sessionId)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig["Jwt:Key"]);


            var token = await Task.Run(() =>
            {
                // reciving the signing credentials that were created using the private key
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature);

                // getting information required to generate a JWT token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("sessionId", sessionId),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = credentials,
                    Issuer = _appConfig["Jwt:Issuer"], // Add this line
                    Audience = _appConfig["Jwt:Audience"],
                };
                return handler.CreateToken(tokenDescriptor);
            });
            
            return handler.WriteToken(token);
        }
    }
}
