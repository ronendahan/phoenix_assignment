using GitHubRepositories.Entities;

namespace GitHubRepositories.Services
{
    public interface IBookmark 
    {
        Task<IEnumerable<RepositoryItem>?> GetBookmarks(string sessionId);
        Task<int> AddBookmark(string sessionId, RepositoryItem bookmark);
        Task<int> RemoveBookmark(string sessionId, int bookMarkId);
    }

    public class BookmarkService: IBookmark
    {
        private readonly IUserSession _sessionService;
        public BookmarkService(IUserSession sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IEnumerable<RepositoryItem>?> GetBookmarks(string sessionId)
        {
            UserSession? userSession = await _sessionService.GetSessionData(sessionId);
            return userSession != null ? userSession?.Bookmarks?.Values.AsEnumerable(): Enumerable.Empty<RepositoryItem>();
        }

        public async Task<int> AddBookmark(string sessionId, RepositoryItem bookmark)
        {
            UserSession? userSession = await _sessionService.GetSessionData(sessionId);
            if (userSession != null)
            {
                userSession?.Bookmarks?.Add(bookmark.Id, bookmark);
                await _sessionService.UpdateSessionData(sessionId, userSession);
            }
            
            return bookmark.Id;
        }

        public async Task<int> RemoveBookmark(string sessionId, int bookmarkId)
        {
            UserSession? userSession = await _sessionService.GetSessionData(sessionId);
            if (userSession != null)
            {
                userSession?.Bookmarks?.Remove(bookmarkId);
                await _sessionService.UpdateSessionData(sessionId, userSession);
            }
            
            return bookmarkId;
        }
    }
}
