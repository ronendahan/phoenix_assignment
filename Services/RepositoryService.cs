using GitHubRepositories.Entities;
using System.Text;

namespace GitHubRepositories.Services
{
    public interface IRepository {
        Task<Repository?> GetRepositories(string keyword);
    }

    public class RepositoryService : IRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RepositoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Repository?> GetRepositories(string keyword)
        {
            var httpClient = _httpClientFactory.CreateClient("github");
            StringBuilder sb = new StringBuilder("?q=");
            sb.Append(keyword);
            var response = await httpClient.GetAsync(sb.ToString());
            response.EnsureSuccessStatusCode();
            return await httpClient.GetFromJsonAsync<Repository>(sb.ToString());
        }
    }
}
