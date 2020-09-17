using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// This class is used to implement all input validation operations for Reminder CRUD operations
    /// </summary>
    public class ReminderService : IReminderService
    {
        /// <summary>
        /// readonly property for repository
        /// </summary>
        readonly IReminderRepository repository;

        /// <summary>
        /// Paramterised constructor for injecting repository
        /// </summary>
        /// <param name="repository"></param>
        public ReminderService(IReminderRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method to add a reminder to a news
        /// </summary>
        /// <param name="reminder">The reminder object to be added</param>
        /// <returns>The reminder that was added</returns>
        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            Reminder presentRem = await repository.GetReminderByNewsId(reminder.NewsId);
            if (presentRem != null)
            {
                throw new ReminderAlreadyExistsException($"This news: {reminder.NewsId} already have a reminder");
            }
            else
            {
                return await repository.AddReminder(reminder);
            }
        }

        /// <summary>
        /// Method for fetching reminder based on the news id
        /// </summary>
        /// <param name="newsId">The id of the news whose reminder is to be fetched</param>
        /// <returns>The reminder corressponding to the news id</returns>
        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            Reminder reminder = await repository.GetReminderByNewsId(newsId);
            if (reminder != null)
            {
                return reminder;
            }
            throw new ReminderNotFoundException($"No reminder found for news: {newsId}");
        }

        /// <summary>
        /// Method to remove a reminder
        /// </summary>
        /// <param name="reminderId">The id of the reminder to be removed</param>
        /// <returns>True if reminder was removed</returns>
        public async Task<bool> RemoveReminder(int reminderId)
        {
            Reminder reminder = await repository.GetReminder(reminderId);
            if (reminder != null)
            {
                return await repository.RemoveReminder(reminder);
            }
            throw new ReminderNotFoundException($"No reminder found with id: {reminderId}");
        }
    }
}
