using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderService by inheriting IReminderService
    public class ReminderService
    {
        /*
       * ReminderRepository should  be injected through constructor injection. 
       * Please note that we should not create ReminderRepository object using the new keyword
       */
        public ReminderService(IReminderRepository reminderRepository)
        {
            
        }
        /* Implement all the methods of respective interface asynchronously*/

        // Implement AddReminder method which should be used to save a new reminder.    

        // Implement GetReminderByNewsId method which should be used to get all reminder by newsId.

        // Implement RemoveReminder method which method should be used to delete an existing reminder withits Id

        // Throw your own custom Exception whereever its required in AddReminder,GetReminderByNewsId and RemoveReminder 
        // functionalities
    }
}
