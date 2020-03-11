using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /*
	 * Should not modify this interface. You have to implement these methods of interface 
     * in corresponding Implementation classes
	 */
    public interface IReminderRepository
    {
        Task<Reminder> AddReminder(Reminder reminder);
        Task<bool> RemoveReminder(Reminder reminder);
        Task<Reminder> GetReminder(int reminderId);
        Task<Reminder> GetReminderByNewsId(int newsId);
    }
}
