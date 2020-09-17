using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;
namespace NewsAPI.Controllers
{
    /// <summary>
    /// Api controller for handling Http requests regarding news entity
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// readonly property for the service class required
        /// </summary>
        readonly INewsService newsService;

        /// <summary>
        /// Paramterised constructor for injecting service class property
        /// </summary>
        /// <param name="newsService"></param>
        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        /// <summary>
        /// Method for fetching all news created by a specific user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET: api/News
        /// </remarks>
        /// <param name="userId">The id of the user whose news are to be fetched</param>
        /// <returns>The list of news created by the mentioned user</returns>
        /// <response code="200">If the news list was fetched successfuly</response>
        /// <response code="404">If no news were found for the user</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string userId)
        {
            return Ok(await newsService.GetAllNews(userId));
        }

        /// <summary>
        /// Method to get a specific news by its id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET: api/News/3
        /// </remarks>
        /// <param name="newsId">The id of the news to be fetched</param>
        /// <returns>The news object corresponding to the mentioned id</returns>
        /// <response code="200">If the news was fetched succesfuly</response>
        /// <response code="404">If the news was not found</response>
        /// <response code="500">If some issue occurred</response>
        [HttpGet("{newsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int newsId)
        {
            return Ok(await newsService.GetNewsById(newsId));
        }

        /// <summary>
        /// Method to add a news
        /// </summary>
        /// <param name="news">The news object that is to be added</param>
        /// <returns>Returns the added news</returns>
        /// <response code="201">If the news was added successfuly</response>
        /// <response code="409">If the news already exists</response>
        /// <response code="500">If some issue occured</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(News news)
        {
            news = await newsService.AddNews(news);
            return Created("api/news", news);
        }

        /// <summary>
        /// Method to delete a news
        /// </summary>
        /// <param name="id">The id of news to be deleted</param>
        /// <returns>True if news was deleted</returns>
        /// <response code="200">If news was deleted successfuly</response>
        /// <response code="404">If news was not found</response>
        /// <response code="500">If some error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await newsService.RemoveNews(id));
        }
    }
}
