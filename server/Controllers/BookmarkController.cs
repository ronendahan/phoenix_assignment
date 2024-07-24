using GitHubRepositories.Entities;
using GitHubRepositories.Helpers;
using GitHubRepositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRepositories.Controllers
{
    [Route("api/bookmarks")]
    [ApiController]
    [AuthTokenAttribute]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmark _bookmarkService;

        public BookmarkController(IBookmark bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepositoryItem>>> GetBookmarks()
        {
            var sessionId = (string?)HttpContext.Items["sessionId"];
            var bookmarks = await _bookmarkService.GetBookmarks(sessionId);
            return Ok(new { bookmarks });
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBookmarks([FromBody] RepositoryItem bookmark)
        {
            var sessionId = (string?)HttpContext.Items["sessionId"];
            var bookmarkId = await _bookmarkService.AddBookmark(sessionId, bookmark);
            return Ok(new { bookmark = bookmarkId });
        }

        [HttpDelete("{bookmark_id:int}")]
        public async Task<ActionResult<int>> RemoveBookmarks(int bookmark_id)
        {
            var sessionId = (string?)HttpContext.Items["sessionId"];
            var bookmarkId = await _bookmarkService.RemoveBookmark(sessionId, bookmark_id);
            return Ok(new { bookmark = bookmarkId });
        }
    }
}
