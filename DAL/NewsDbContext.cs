using Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    /// <summary>
    /// Class that facilitates communication with database and its creation based on entitiy framework
    /// </summary>
    public class NewsDbContext : DbContext
    {
        /// <summary>
        /// The property representing Users table
        /// </summary>
        public DbSet<UserProfile> Users { get; set; }

        /// <summary>
        /// The property representing NewsList table
        /// </summary>
        public DbSet<News> NewsList { get; set; }

        /// <summary>
        /// The property representing Reminders table
        /// </summary>
        public DbSet<Reminder> Reminders { get; set; }

        /// <summary>
        /// Parametrised constructor
        /// </summary>
        /// <param name="options"></param>
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Method for applying constraints and setting properties of table columns
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasKey(u => u.UserId);
            modelBuilder.Entity<UserProfile>().Property(u => u.UserId).ValueGeneratedNever();
            modelBuilder.Entity<UserProfile>().Property(u => u.UserId).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.Contact).IsRequired();
            modelBuilder.Entity<UserProfile>().Property(u => u.CreatedAt).IsRequired();

            modelBuilder.Entity<News>().HasKey(n => n.NewsId);
            modelBuilder.Entity<News>().HasOne<UserProfile>().WithMany().HasForeignKey(n => n.CreatedBy);
            modelBuilder.Entity<News>().Property(n => n.NewsId).ValueGeneratedNever();
            modelBuilder.Entity<News>().Property(n => n.NewsId).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.Title).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.Content).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.PublishedAt).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.CreatedBy).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.Url).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.UrlToImage).IsRequired();

            modelBuilder.Entity<Reminder>().HasKey(r => r.ReminderId);
            modelBuilder.Entity<Reminder>().HasOne<News>().WithMany().HasForeignKey(r => r.NewsId);
            //modelBuilder.Entity<Reminder>().Property(r => r.ReminderId).ValueGeneratedNever();
            modelBuilder.Entity<Reminder>().Property(r => r.ReminderId).IsRequired();
            modelBuilder.Entity<Reminder>().Property(r => r.Schedule).IsRequired();
            modelBuilder.Entity<Reminder>().Property(r => r.NewsId).IsRequired();
        }
    }
}

