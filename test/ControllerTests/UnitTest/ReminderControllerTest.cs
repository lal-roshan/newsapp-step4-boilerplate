using System;
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
    public class ReminderControllerTest
    {
        [Fact]
        public async Task GetShouldReturnOk()
        {
            int newsId = 101;
            Reminder reminder = new Reminder {ReminderId=1, NewsId = 101, Schedule = DateTime.Now.AddDays(5) };
            var mockService = new Mock<IReminderService>();
            mockService.Setup(svc => svc.GetReminderByNewsId(newsId)).Returns(Task.FromResult(reminder));
            var controller = new ReminderController(mockService.Object);

            var actual = await controller.Get(newsId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<Reminder>(actionResult.Value);
        }
        [Fact]
        public async Task GetShouldReturnNotFound()
        {
            int newsId = 102;
            var mockService = new Mock<IReminderService>();
            mockService.Setup(svc => svc.GetReminderByNewsId(newsId)).Throws(new ReminderNotFoundException($"No reminder found for news: {newsId}"));
            var controller = new ReminderController(mockService.Object);

            var actual = await Assert.ThrowsAsync<ReminderNotFoundException>(()=> controller.Get(newsId));
            Assert.Equal($"No reminder found for news: {newsId}",actual.Message);
        }

        [Fact]
        public async Task PostShouldReturnCreated()
        {
            var mockService = new Mock<IReminderService>();
            Reminder reminder = new Reminder { NewsId=102, Schedule=DateTime.Now.AddDays(1) };
            Reminder addedReminder = new Reminder { ReminderId=2, NewsId = 102, Schedule = DateTime.Now.AddDays(1) };
            mockService.Setup(svc => svc.AddReminder(reminder)).Returns(Task.FromResult(addedReminder));
            var controller = new ReminderController(mockService.Object);

            var actual = await controller.Post(reminder);
            var actionResult = Assert.IsType<CreatedResult>(actual);
            var actionValue = Assert.IsAssignableFrom<Reminder>(actionResult.Value);
            Assert.Equal(2, actionValue.ReminderId);
        }
        [Fact]
        public async Task PostShouldReturnConflict()
        {
            var mockService = new Mock<IReminderService>();
            Reminder reminder = new Reminder { NewsId = 101, Schedule = DateTime.Now.AddDays(1) };
            mockService.Setup(svc => svc.AddReminder(reminder)).Throws(new ReminderAlreadyExistsException($"This news: {reminder.NewsId} already have a reminder"));
            var controller = new ReminderController(mockService.Object);

            var actual = await Assert.ThrowsAsync<ReminderAlreadyExistsException>(()=> controller.Post(reminder));
            Assert.Equal($"This news: {reminder.NewsId} already have a reminder",actual.Message);
        }

        [Fact]
        public async Task DeleteShouldReturnOk()
        {
            int reminderId = 2;
            var mockService = new Mock<IReminderService>();
            mockService.Setup(svc => svc.RemoveReminder(reminderId)).Returns(Task.FromResult(true));
            var controller = new ReminderController(mockService.Object);

            var actual = await controller.Delete(reminderId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.True(Convert.ToBoolean(actionResult.Value));
        }

        [Fact]
        public async Task DeleteShouldReturnNotFound()
        {
            int reminderId = 3;
            var mockService = new Mock<IReminderService>();
            mockService.Setup(svc => svc.RemoveReminder(reminderId)).Throws(new ReminderNotFoundException($"No reminder found with id: {reminderId}"));
            var controller = new ReminderController(mockService.Object);

            var actual = await Assert.ThrowsAsync<ReminderNotFoundException>(()=> controller.Delete(reminderId));
            Assert.Equal($"No reminder found with id: {reminderId}",actual.Message);
        }
    }
}
