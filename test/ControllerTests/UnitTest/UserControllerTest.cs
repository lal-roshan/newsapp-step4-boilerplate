using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Entities;
using Service;
using NewsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

namespace Test.ControllerTests.UnitTest
{
    public class UserControllerTest
    {
        [Fact]
        public async Task PostShouldReturnCreated()
        {
            UserProfile user = new UserProfile { UserId = "John", FirstName = "Johnson", LastName = "dsouza", Contact = "7869543210", Email = "john@gmail.com", CreatedAt = DateTime.Now };
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.AddUser(user)).Returns(Task.FromResult(true));
            var controller = new UserController(mockService.Object);

            var actual = await controller.Post(user);
            var actionResult = Assert.IsType<CreatedResult>(actual);
            Assert.True(Convert.ToBoolean(actionResult.Value));
        }

        [Fact]
        public async Task GetShouldReturnOk()
        {
            UserProfile user = new UserProfile { UserId = "John", FirstName = "Johnson", LastName = "dsouza", Contact = "7869543210", Email = "john@gmail.com", CreatedAt = DateTime.Now };
            string userId = "John";
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.GetUser(userId)).Returns(Task.FromResult(user));
            var controller = new UserController(mockService.Object);

            var actual = await controller.Get(userId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<UserProfile>(actionResult.Value);
        }

        [Fact]
        public async Task DeleteShouldReturnOk()
        {
            string userId = "John";
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.DeleteUser(userId)).Returns(Task.FromResult(true));
            var controller = new UserController(mockService.Object);

            var actual = await controller.Delete(userId);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.True(Convert.ToBoolean(actionResult.Value));
        }

        [Fact]
        public async Task PutShouldReturnOk()
        {
            string userId = "Jack";
            UserProfile user = new UserProfile { UserId = "Jack", FirstName = "Jackson", LastName = "James", Contact = "9812345670", Email = "jack@ymail.com", CreatedAt = DateTime.Now };
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.UpdateUser(userId,user)).Returns(Task.FromResult(true));
            var controller = new UserController(mockService.Object);

            var actual = await controller.Put(userId,user);
            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.True(Convert.ToBoolean(actionResult.Value));
        }

        [Fact]
        public async Task PostShouldReturnConflict()
        {
            UserProfile user = new UserProfile { UserId = "John", FirstName = "Johnson", LastName = "dsouza", Contact = "7869543210", Email = "john@gmail.com", CreatedAt = DateTime.Now };
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.AddUser(user)).Throws(new UserAlreadyExistsException($"{user.UserId} already exists"));
            var controller = new UserController(mockService.Object);

            var actual = await Assert.ThrowsAsync<UserAlreadyExistsException>(()=> controller.Post(user));
            Assert.Equal($"{user.UserId} already exists", actual.Message);
        }
       
        [Fact]
        public async Task GetShouldReturnNotFound()
        {
            string userId = "Kevin";
            var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.GetUser(userId)).Throws(new UserNotFoundException($"{userId} doesn't exist"));
            var controller = new UserController(mockService.Object);

            var actual = await Assert.ThrowsAsync<UserNotFoundException>(()=> controller.Get(userId));
            Assert.Equal($"{userId} doesn't exist",actual.Message);
        }

        
       [Fact]
       public async Task DeleteShouldReturnNotFound()
       {
           string userId = "John";
           var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.DeleteUser(userId)).Throws(new UserNotFoundException($"{userId} doesn't exist"));
           var controller = new UserController(mockService.Object);

           var actual = await Assert.ThrowsAsync<UserNotFoundException>(()=> controller.Delete(userId));
           Assert.Equal($"{userId} doesn't exist", actual.Message);
        }
       [Fact]
       public async Task PutShouldReturnNotFound()
       {
           string userId = "Jack";
           UserProfile user = new UserProfile { UserId = "Jack", FirstName = "Jackson", LastName = "James", Contact = "9812345670", Email = "jack@ymail.com", CreatedAt = DateTime.Now };
           var mockService = new Mock<IUserService>();
            mockService.Setup(svc => svc.UpdateUser(userId, user)).Throws(new UserNotFoundException($"{userId} doesn't exist"));
           var controller = new UserController(mockService.Object);

           var actual = await Assert.ThrowsAsync<UserNotFoundException>(()=> controller.Put(userId, user));
            Assert.Equal($"{userId} doesn't exist", actual.Message);
        }
    }
}
