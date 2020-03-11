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
    public class NewsController : ControllerBase
    {
        /*
        * NewsService should  be injected through constructor injection. 
        * Please note that we should not create service object using the new keyword
        */
        public NewsController(INewsService newsService)
        {
          
        }
        /* Implement HttpVerbs and Functionalities asynchronously*/
        /*
         * Define a handler method which will get us the news by a userId.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the news found successfully.
         * This handler method should map to the URL "/api/news/{userId}" using HTTP GET method
         */

        /*
        * Define a handler method which will get us the news by a newsId.
        * 
        * This handler method should return any one of the status messages basis on
        * different situations: 
        * 1. 200(OK) - If the news found successfully.
        * This handler method should map to the URL "/api/news/{newsId:int}" using HTTP GET method
        */

        /*
         * Define a handler method which will create a specific news by reading the
         * Serialized object from request body and save the news details in a News table
         * in the database.
         * 
         * Please note that AddNews method should add a news and also handle the exception using 
         * ExceptionHandler.This handler method should return any one of the status messages 
         * basis on different situations: 
         * 1. 201(CREATED) - If the news created successfully. 
         * 2. 409(CONFLICT) - If the newsId conflicts with any existing newsid
         * 
         * This handler method should map to the URL "/api/news" using HTTP POST method
         */

        /*
         * Define a handler method which will delete a news from a database.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the news deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the news with specified newsId is not found.
         * 
         * This handler method should map to the URL "/api/news/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid newsId without {}
         */
    }
}
