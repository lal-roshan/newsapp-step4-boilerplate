using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Service;
using Entities;
using NewsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

namespace Test.ControllerTests.UnitTest
{
    public class NewsControllerTest
    {
        [Fact]
        public async Task GetShouldReturnOk()
        {
            string userId = "Jack";
            var mockService = new Mock<INewsService>();
            mockService.Setup(svc => svc.GetAllNews(userId)).Returns(Task.FromResult(newsList));
            var controller = new NewsController(mockService.Object);

            var actual = await controller.Get(userId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<IList<News>>(actionResult.Value);
        }
        [Fact]
        public async Task GetByNewsIdShouldReturnOk()
        {
            int newsId = 101;
            var mockService = new Mock<INewsService>();
            var news = new News { NewsId = 101, Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack", Url = null };
            mockService.Setup(svc => svc.GetNewsById(newsId)).Returns(Task.FromResult(news));
            var controller = new NewsController(mockService.Object);

            var actual = await controller.Get(newsId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<News>(actionResult.Value);
        }

        [Fact]
        public async Task PostShouldReturnCreated()
        {
            var mockService = new Mock<INewsService>();
            News news = new News { Title = "chandrayaan2-spacecraft", Content = "The Lander of Chandrayaan-2 was named Vikram after Dr Vikram A Sarabhai", PublishedAt = DateTime.Now, CreatedBy = "Jack" };
            News addedNews = new News { NewsId=103, Title = "chandrayaan2-spacecraft", Content = "The Lander of Chandrayaan-2 was named Vikram after Dr Vikram A Sarabhai", PublishedAt = DateTime.Now, CreatedBy = "Jack" };
            mockService.Setup(svc => svc.AddNews(news)).Returns(Task.FromResult(addedNews));
            var controller = new NewsController(mockService.Object);

            var actual = await controller.Post(news);
            var actionResult = Assert.IsType<CreatedResult>(actual);
            var actionValue=Assert.IsAssignableFrom<News>(actionResult.Value);
            Assert.Equal(103,actionValue.NewsId);
        }

        [Fact]
        public async Task DeleteShouldReturnOk()
        {
            int newsId = 103;
            var mockService = new Mock<INewsService>();
            mockService.Setup(svc => svc.RemoveNews(newsId)).Returns(Task.FromResult(true));
            var controller = new NewsController(mockService.Object);

            var actual = await controller.Delete(newsId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.True(Convert.ToBoolean(actionResult.Value));
        }

        [Fact]
        public async Task GetShouldReturnNotFound()
        {
            string userId = "John";
            var mockService = new Mock<INewsService>();
            mockService.Setup(svc => svc.GetAllNews(userId)).Throws(new NewsNotFoundException($"No news found for user: {userId}"));
            var controller = new NewsController(mockService.Object);

            var actual = await Assert.ThrowsAsync<NewsNotFoundException>(()=> controller.Get(userId));
            Assert.Equal($"No news found for user: {userId}",actual.Message);
        }
        [Fact]
        public async Task GetByNewsIdShouldReturnNotFound()
        {
            int newsId = 103;
            var mockService = new Mock<INewsService>();
            mockService.Setup(svc => svc.GetNewsById(newsId)).Throws(new NewsNotFoundException($"No news found with Id: {newsId}"));
            var controller = new NewsController(mockService.Object);

            var actual = await Assert.ThrowsAsync<NewsNotFoundException>(()=> controller.Get(newsId));
            Assert.Equal($"No news found with Id: {newsId}", actual.Message);
        }

        [Fact]
        public async Task PostShouldReturnConflict()
        {
            var mockService = new Mock<INewsService>();
            News news = new News { Title = "chandrayaan2-spacecraft", Content = "The Lander of Chandrayaan-2 was named Vikram after Dr Vikram A Sarabhai", PublishedAt = DateTime.Now, CreatedBy = "Jack" };
            mockService.Setup(svc => svc.AddNews(news)).Throws(new NewsAlreadyExistsException($"This news is already added"));
            var controller = new NewsController(mockService.Object);

            var actual = await Assert.ThrowsAsync<NewsAlreadyExistsException>(()=> controller.Post(news));
            Assert.Equal($"This news is already added", actual.Message);
        }

        [Fact]
        public async Task DeleteShouldReturnNotFound()
        {
            int newsId = 103;
            var mockService = new Mock<INewsService>();
            mockService.Setup(svc => svc.RemoveNews(newsId)).Throws(new NewsNotFoundException($"No news found with Id: {newsId}"));
            var controller = new NewsController(mockService.Object);

            var actual =await Assert.ThrowsAsync<NewsNotFoundException>(()=>controller.Delete(newsId));
            Assert.Equal($"No news found with Id: {newsId}", actual.Message);
        }

        List<News> newsList = new List<News> {
        new News { NewsId = 101, Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack",Url=null },
        new News { NewsId = 102, Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack" }
        };
    }
}
