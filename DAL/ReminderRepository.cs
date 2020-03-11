using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderRepository by inheriting IReminderRepository
    //ReminderRepository class is used to implement all Data access operations
    public class ReminderRepository
    {
        private readonly NewsDbContext context;
        
        public ReminderRepository(NewsDbContext dbContext)
        {
            
        }
        //Implement the methods of interface Asynchronously.
        // Implement AddReminder method which should be used to save a new reminder.    
        // Implement RemoveReminder method which method should be used to delete an existing reminder.
        // Implement GetReminderByNewsId method which should be used to get all reminder by newsId.
        // Implement GetReminder method which should be used to get a reminder by reminderId.
        // Implement UpdateReminder method which should be used to update an existing reminder.
    }
}
