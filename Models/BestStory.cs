using System.Text.Json.Serialization;

namespace GitHubBestStories.Models
{
    public class BestStory
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Uri { get; set; }

        [JsonPropertyName("by")]
        public string PostedBy { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        //[JsonPropertyName("CommentCount")]
        //public int CommentCount { get; set; }
    }
}
