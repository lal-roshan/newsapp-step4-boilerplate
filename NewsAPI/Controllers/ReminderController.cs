using Entities;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Aspect;
using Service;
using System.Threading.Tasks;
namespace NewsAPI.Controllers
{
    /*
    * As in this assignment, we are working with creating RESTful web service, hence annotate
    * the class with [ApiController] annotation and define the controller level route as per REST Api standard.
    * and also use ServiceFilter to handle the exception logic using ExceptionHandler
    */
    public class ReminderController : ControllerBase
    {
        /*
        * ReminderService should  be injected through constructor injection. 
        * Please note that we should not create Reminderservice object using the new keyword
        */
       
        public ReminderController(IReminderService reminderService)
        {
            
        }
        /* Implement HttpVerbs and its Functionalities asynchronously*/

        /*
        * Define a handler method which will get us the reminders by a newsId.
        * 
        * This handler method should return any one of the status messages basis on
        * different situations: 
        * 1. 200(OK) - If the reminder found successfully.
        * 
        * This handler method should map to the URL "/api/reminder/{newsId}" using HTTP GET method
        */

        /*
	     * Define a handler method which will create a reminder by reading the
	     * Serialized reminder object from request body and save the reminder in
	     * reminder table in database. Please note that the reminderId has to be unique
	     * and newsId should be taken as the GetReminderByNewsId for the
	     * reminder.
         * This handler method should return any one of the status messages
	     * basis on different situations: 
         * 1. 201(CREATED - In case of successful creation of the reminder 
         * 2. 409(CONFLICT) - In case of duplicate reminder ID
	     * This handler method should map to the URL "/api/reminder" using HTTP POST
	     * method".
	     */


        /*
         * Define a handler method which will delete a reminder from a database.
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the reminder deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the reminder with specified reminderId is  not found. 
         * This handler method should map to the URL "/api/reminder/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid reminderId without {}
         */
        
    }
}
