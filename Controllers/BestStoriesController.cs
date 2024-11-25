using GitHubBestStories.Models;
using GitHubBestStories.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GitHubBestStories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;
        private readonly IGitHubService _gitHubService;

        public BestStoriesController(ILogger<BestStoriesController> logger, IGitHubService gitHubService)
        {
            _logger = logger;
            _gitHubService = gitHubService;
        }

        [HttpGet]
        [Route("getbeststories")]
        public async Task<ActionResult<IEnumerable<BestStory>>> GetBestStories(int n)
        {
            var repositoryBestStoryIds = await _gitHubService.GetRepositoryBestStoryIds();

            var bestStories = new List<BestStory>();

            if(repositoryBestStoryIds.Count > 0)
            {
                var tasks = new List<Task<BestStory>>();

                Func<object?, BestStory> getBestStoryById = (id) => { return _gitHubService.GetBestStoryByIdSync((int)id); };

                foreach (var bestStoryId in repositoryBestStoryIds)
                {
                    Task<BestStory> task = Task<BestStory>.Factory.StartNew(getBestStoryById, bestStoryId);
                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var task in tasks)
                {
                    bestStories.Add(task.Result);
                }

                var topBestStories = bestStories.OrderByDescending(b => b.Score).Take(n).ToList();

                return topBestStories;
            }

            return bestStories.ToArray();
        }
    }
}
