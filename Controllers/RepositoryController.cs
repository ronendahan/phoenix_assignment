using GitHubRepositories.Entities;
using GitHubRepositories.Helpers;
using GitHubRepositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRepositories.Controllers
{
    [Route("api/repositories")]
    [ApiController]
    [AuthTokenAttribute]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepository _repositoryService;

        public RepositoryController(IRepository repositoryService) {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repository>>> GetRepositories([FromQuery] string q)
        {
            var repository = await _repositoryService.GetRepositories(q);
            return Ok(new { repository = repository });
        }
    }
}
