using GitHubRepositories.Entities;
using System.Text.Json;

namespace GitHubRepositories.Services
{
    public interface IUserSession
    {
        public Task<string> CreateSession();
        public Task<string> UpdateSessionData(string sessionId, UserSession? data);
        public Task<UserSession?> GetSessionData(string sessionId);
    }

    public class UserSessionService: IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreateSession()
        {
            string sessionId = await Task.Run(() => {
                UserSession userSession = new UserSession();
                userSession.SessionId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext?.Session.SetString(userSession.SessionId, JsonSerializer.Serialize(userSession));
                return userSession.SessionId;
            });

            return sessionId;
        }

        public async Task<string> UpdateSessionData(string sessionId, UserSession? data)
        {
            string updatedSessionId = await Task.Run(() => {
                _httpContextAccessor.HttpContext?.Session.SetString(sessionId, JsonSerializer.Serialize(data));
                return sessionId;
            });
            return updatedSessionId;
        }

        public async Task<UserSession?> GetSessionData(string sessionId)
        {
            UserSession? userSession = await Task.Run(() => {
                string? sessionData = _httpContextAccessor.HttpContext?.Session.GetString(sessionId);
                return sessionData != null ? JsonSerializer.Deserialize<UserSession>(sessionData) : null;

            });
            return userSession;
        }
    }
}
