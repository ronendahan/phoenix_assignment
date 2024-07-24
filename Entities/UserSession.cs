namespace GitHubRepositories.Entities
{
    public class UserSession
    {
        public string? SessionId { get; set; }
        public Dictionary<int, RepositoryItem>? Bookmarks { get; set; }
    }
}
