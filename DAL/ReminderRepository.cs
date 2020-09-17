using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace DAL
{
    /// <summary>
    /// Class which fascilitates the CRUD operations on Reminders table
    /// </summary>
    public class ReminderRepository : IReminderRepository
    {
        /// <summary>
        /// Readonly property for dbcontext
        /// </summary>
        readonly NewsDbContext dbContext;

        /// <summary>
        /// Parametrised constructor for injecting dbcontext property
        /// </summary>
        /// <param name="dbContext"></param>
        public ReminderRepository(NewsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method to add a reminder into the database
        /// </summary>
        /// <param name="reminder">The reminder object that is to be added</param>
        /// <returns>The added reminder</returns>
        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            await dbContext.Reminders.AddAsync(reminder);
            await dbContext.SaveChangesAsync();
            return reminder;
        }

        /// <summary>
        /// Method to fetch a reminder based on its id
        /// </summary>
        /// <param name="reminderId">The id of the reminder that is to be fetched</param>
        /// <returns>The reminder object with the mentioned id</returns>
        public async Task<Reminder> GetReminder(int reminderId)
        {
            return await dbContext.Reminders.Where(rem => rem.ReminderId == reminderId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to fetch a reminder based on its news id
        /// </summary>
        /// <param name="newsId">The id of the news whose reminder is to be fetched</param>
        /// <returns>The reminder object with the mentioned news id</returns>
        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            return await dbContext.Reminders.Where(rem => rem.NewsId == newsId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to remove a reminder from the table
        /// </summary>
        /// <param name="reminder">The reminder object that is to be removed</param>
        /// <returns>True if removal was successful</returns>
        public async Task<bool> RemoveReminder(Reminder reminder)
        {
            dbContext.Reminders.Remove(reminder);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
