using GitHubBestStories.Models;
using System.Text.Json;

namespace GitHubBestStories.Services
{
    public interface IGitHubService
    {
        Task<RepositoryBestStoryIds> GetRepositoryBestStoryIds();

        Task<BestStory> GetBestStoryById(int bestStoryId);

        BestStory GetBestStoryByIdSync(int bestStoryId);
    }

    public class GitHubService: IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RepositoryBestStoryIds> GetRepositoryBestStoryIds()
        {
            var url = $"{_httpClient.BaseAddress}/beststories.json";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
            var responseBody = await response.Content.ReadAsStringAsync();
            var repositoryBestStoryIds = JsonSerializer.Deserialize<RepositoryBestStoryIds>(responseBody);

            if (repositoryBestStoryIds != null && repositoryBestStoryIds.Count > 0)
            {
                return repositoryBestStoryIds;
            }

            return new RepositoryBestStoryIds();
        }

        public async Task<BestStory> GetBestStoryById(int bestStoryId)
        {
            var url = $"{_httpClient.BaseAddress}/item/{bestStoryId}.json";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
            var responseBody = await response.Content.ReadAsStringAsync();
            var repositoryBestStoryIds = JsonSerializer.Deserialize<BestStory>(responseBody);

            //if (repositoryBestStoryIds != null && repositoryBestStoryIds.Count > 0)
            //{
            //    return repositoryBestStoryIds;
            //}

            return new BestStory();
        }

        public BestStory GetBestStoryByIdSync(int bestStoryId)
        {
            var url = $"{_httpClient.BaseAddress}/item/{bestStoryId}.json";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var responseTask = Task.Run(
                async () => await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, 
                    CancellationToken.None));

            var responseBody = responseTask.Result.Content.ReadAsStringAsync().Result;
            var bestStory = JsonSerializer.Deserialize<BestStory>(responseBody);

            if(bestStory != null)
            {
                return bestStory;
            }

            return new BestStory();
        }
    }
}
