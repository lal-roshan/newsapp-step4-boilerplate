using DAL;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsAPI;
using System;
using System.Linq;
using Xunit;

namespace Test.ControllerTests.IntegrationTest
{
    public class NewsWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<NewsDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context (KeepNoteContext) using an in-memory database for testing.
                services.AddDbContext<NewsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("NewsDB_Test");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var newsDb = scopedServices.GetRequiredService<NewsDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<NewsWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    newsDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        //Adding User
                        newsDb.Users.Add(new UserProfile { UserId = "Jack", FirstName = "Jackson", LastName = "James", Contact = "9812345670", Email = "jack@ymail.com", CreatedAt = DateTime.Now });
                        newsDb.SaveChanges();
                        
                        //Adding News
                        newsDb.NewsList.Add(new News { NewsId = 101, Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack",Url=null });
                        newsDb.NewsList.Add(new News { NewsId = 102, Title = "2020 FIFA U-17 Women World Cup", Content = "The tournament will be held in India between 2 and 21 November 2020", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack" });
                        newsDb.SaveChanges();

                        //Adding Reminder
                        newsDb.Reminders.Add(new Reminder { NewsId = 101, Schedule = DateTime.Now.AddDays(5) });
                        newsDb.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    [CollectionDefinition("News Fixture")]
    public class DBCollection : ICollectionFixture<NewsWebApplicationFactory<Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
