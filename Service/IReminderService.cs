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
    public interface IReminderService
    {
        Task<Reminder> AddReminder(Reminder reminder);
        Task<bool> RemoveReminder(int reminderId);
        Task<Reminder> GetReminderByNewsId(int newsId);
    }
}
