using GitHubBestStories.Services;

namespace GitHubBestStories.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseApiUrl;
            services.AddSingleton<IGitHubService>(new GitHubService(httpClient));

            return services;
        }
    }
}
