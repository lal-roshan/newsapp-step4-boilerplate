using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /*
	 * Should not modify this interface. You have to implement these methods of interface 
     * in corresponding Implementation classes
	 */
    public interface INewsService
    {
        Task<News> AddNews(News news);
        Task<News> GetNewsById(int newsId);
        Task<List<News>> GetAllNews(string userId);
        Task<bool> RemoveNews(int newsId);
    }
}
