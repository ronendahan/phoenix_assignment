using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace GitHubRepositories.Entities
{
    public class RepositoryItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Owner 
    {
        [JsonPropertyName("avatar_url")]
        public string Avatar { get; set; }
    }

    public class Repository {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        [JsonPropertyName("items")]
        public List<RepositoryItem> Items { get; set; }
    }
}
