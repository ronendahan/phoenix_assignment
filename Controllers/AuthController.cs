using GitHubRepositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRepositories.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuth _authService;
        private IUserSession _userSessionService;
        public AuthController(IAuth authService, IUserSession userSessionService)
        {
            _authService = authService;
            _userSessionService = userSessionService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate()
        {
            string sessionId = await _userSessionService.CreateSession();
            string token = await _authService.GenerateToken(sessionId);

            return Ok(new { token = token });
        }
    }
}
