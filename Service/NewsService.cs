using DAL;
using Entities;
using Service.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// This class is used to implement all input validation operations for News CRUD operations
    /// </summary>
    public class NewsService : INewsService
    {
        /// <summary>
        /// readonly property for repository
        /// </summary>
        readonly INewsRepository repository;

        /// <summary>
        /// Paramterised constructor for injecting repository property
        /// </summary>
        /// <param name="repository"></param>
        public NewsService(INewsRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method to add news
        /// </summary>
        /// <param name="news">The news object that is to be added</param>
        /// <returns>The added news</returns>
        public async Task<News> AddNews(News news)
        {
            bool newsExists = await repository.IsNewsExist(news);
            if (!newsExists)
            {
                return await repository.AddNews(news);
            }
            throw new NewsAlreadyExistsException("This news is already added");
        }

        /// <summary>
        /// Method for fetching all news created by a user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>List of all news that the user has created</returns>
        public async Task<List<News>> GetAllNews(string userId)
        {
            List<News> newsList = await repository.GetAllNews(userId);
            if (newsList.Count > 0)
            {
                return newsList;
            }
            throw new NewsNotFoundException($"No news found for user: {userId}");
        }

        /// <summary>
        /// Method to fetch a particular news based on id
        /// </summary>
        /// <param name="newsId">The id of the news to be fetched</param>
        /// <returns>The news corresponding to the id</returns>
        public async Task<News> GetNewsById(int newsId)
        {
            News news = await repository.GetNewsById(newsId);
            if (news != null)
            {
                return news;
            }
            throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }

        /// <summary>
        /// Method to remove a news
        /// </summary>
        /// <param name="newsId">The id of the news to be removed</param>
        /// <returns>True if news was removed</returns>
        public async Task<bool> RemoveNews(int newsId)
        {
            News news = await repository.GetNewsById(newsId);
            if (news != null)
            {
                return await repository.RemoveNews(news);
            }
            throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }
    }
}
