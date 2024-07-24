using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GitHubRepositories.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _appConfig;

        public JwtMiddleware(RequestDelegate next, IConfiguration appConfig)
        {
            _next = next;
            _appConfig = appConfig;
        }

        public async Task Invoke(HttpContext context)
        {
            // Extract token from the Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachSessionToContext(context, token);

            await _next(context);
        }

        private async Task AttachSessionToContext(HttpContext context, string token)
        {
            // validate the token and extract sessionId value
            await Task.Run(() => {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = _appConfig["Jwt:Key"];
                var issuer = _appConfig["Jwt:Issuer"];
                var audience = _appConfig["Jwt:Audience"];

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var sessionId = jwtToken.Claims.First(x => x.Type == "sessionId").Value;
                context.Items["sessionId"] = sessionId;
            });
        }
    }
}
