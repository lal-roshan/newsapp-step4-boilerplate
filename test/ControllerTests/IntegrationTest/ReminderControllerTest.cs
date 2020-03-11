using Entities;
using NewsAPI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Test.ControllerTests.IntegrationTest
{

    [Collection("News Fixture")]
    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    public class ReminderControllerTest
    {
        private readonly HttpClient _client;
        public ReminderControllerTest(NewsWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task GetShouldReturnReminder()
        {
            // The endpoint or route of the controller action.
            int newsId = 101;
            var httpResponse = await _client.GetAsync($"/api/reminder/{newsId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var reminder = JsonConvert.DeserializeObject<Reminder>(stringResponse);
            Assert.Equal(1, reminder.ReminderId);
        }

        [Fact, TestPriority(2)]
        public async Task PostShouldReturnReminder()
        {
            // The endpoint or route of the controller action.
            Reminder reminder = new Reminder { NewsId=102, Schedule=DateTime.Now.AddDays(2) };
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            var httpResponse = await _client.PostAsync($"/api/reminder", reminder, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Reminder>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.Equal(2, actual.ReminderId);
        }
        [Fact, TestPriority(3)]
        public async Task DeleteShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int reminderId = 2;
            var httpResponse = await _client.DeleteAsync($"/api/reminder/{reminderId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(4)]
        public async Task GetShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int newsId = 102;
            var httpResponse = await _client.GetAsync($"/api/reminder/{newsId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"No reminder found for news: {newsId}", stringResponse);
        }

        [Fact, TestPriority(5)]
        public async Task PostShouldReturnConflict()
        {
            // The endpoint or route of the controller action.
            Reminder reminder = new Reminder { NewsId = 101, Schedule = DateTime.Now.AddDays(2) };
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            var httpResponse = await _client.PostAsync($"/api/reminder", reminder, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Conflict, httpResponse.StatusCode);
            Assert.Equal($"This news: {reminder.NewsId} already have a reminder", stringResponse);
        }
        [Fact, TestPriority(6)]
        public async Task DeleteShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int reminderId = 2;
            var httpResponse = await _client.DeleteAsync($"/api/reminder/{reminderId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"No reminder found with id: {reminderId}",stringResponse);
        }

        #region LoggingTest
        [Fact, TestPriority(7)]
        public void LogFileShouldBeCreated()
        {
            string folder = "test";
            var directory = Environment.CurrentDirectory;
            int indexofmyvar = directory.IndexOf(@"bin");
            string rootpath = directory.Substring(0, indexofmyvar - (folder.Length + 1));
            string filepath = Path.Combine(rootpath, @"NewsAPI/LogFile.txt");
            Assert.True(File.Exists(filepath));
        }

        [Fact, TestPriority(8)]
        public void MessageShouldBeLogged()
        {
            string folder = "test";
            var directory = Environment.CurrentDirectory;
            int indexofmyvar = directory.IndexOf(@"bin");
            string rootpath = directory.Substring(0, indexofmyvar - (folder.Length + 1));
            string filepath = Path.Combine(rootpath, @"NewsAPI/LogFile.txt");
            string contents = File.ReadAllText(filepath);
            Assert.True(File.ReadAllText(filepath).Length > 0);
            Assert.True(Regex.Match(contents, "/api/reminder").Success);
            Assert.True(Regex.Match(contents, "/api/reminder/2").Success);
            Assert.True(Regex.Match(contents, "/api/reminder/101").Success);
            Assert.True(Regex.Match(contents, "/api/reminder/102").Success);
        }
        #endregion LoggingTest
    }
}
