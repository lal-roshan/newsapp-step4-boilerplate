using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DAL
{
    /// <summary>
    /// Class helping in implementation of CRUD operations on NewsList table
    /// </summary>
    public class NewsRepository : INewsRepository
    {
        /// <summary>
        /// Readonly property for dbcontext
        /// </summary>
        readonly NewsDbContext dbContext;

        /// <summary>
        /// Parametrised constructor for injecting dbcontext property
        /// </summary>
        /// <param name="dbContext"></param>
        public NewsRepository(NewsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method for adding news to the table
        /// </summary>
        /// <param name="news">The news object that is to be added</param>
        /// <returns>The added news object</returns>
        public async Task<News> AddNews(News news)
        {
            int newsId = 101;
            if (dbContext.NewsList.Count() > 0)
                newsId = dbContext.NewsList.Max(t => t.NewsId) + 1;
            news.NewsId = newsId;
            await dbContext.NewsList.AddAsync(news);
            await dbContext.SaveChangesAsync();
            return news;
        }

        /// <summary>
        /// Method for fetching all news published by a specific user
        /// </summary>
        /// <param name="userId">Id of the user whose news are to be fetched</param>
        /// <returns>List of news added by the user with provided user id</returns>
        public async Task<List<News>> GetAllNews(string userId)
        {
            return await dbContext.NewsList.Where(news => string.Equals(news.CreatedBy, userId)).ToListAsync();
        }

        /// <summary>
        /// Method to get a specific news by its id
        /// </summary>
        /// <param name="newsId">The id of the news to be fetched</param>
        /// <returns>The news with the mentioned id if present</returns>
        public async Task<News> GetNewsById(int newsId)
        {
            return await dbContext.NewsList.Where(news => news.NewsId == newsId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to check whether a news is already present in the database
        /// </summary>
        /// <param name="news">The news object whose existence in the table is to be found out</param>
        /// <returns>True if news exists</returns>
        public async Task<bool> IsNewsExist(News news)
        {
            return await dbContext.NewsList.AnyAsync(n =>
            string.Equals(n.Title, news.Title) &&
            string.Equals(n.Content, news.Content) &&
            string.Equals(n.CreatedBy, news.CreatedBy));
        }

        /// <summary>
        /// Method to remove a news from the table
        /// </summary>
        /// <param name="news">The news object that is to be removed</param>
        /// <returns>True if remove was successful</returns>
        public async Task<bool> RemoveNews(News news)
        {
            dbContext.NewsList.Remove(news);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
