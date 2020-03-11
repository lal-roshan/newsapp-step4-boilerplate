using DAL;
using Entities;
using Service.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e NewsService by inheriting INewsService
    public class NewsService
    {
        /*
        * NewsRepository should  be injected through constructor injection. 
        * Please note that we should not create NewsRepository object using the new keyword
        */

        public NewsService(INewsRepository newsRepository)
        {
          
        }
        /* Implement all the methods of respective interface asynchronously*/

        /* Implement AddNews method to add the new news details*/
        /* Implement GetAllNews method to get the news details of perticular userid*/
        /* Implement GetNewsById method to get the existing news by news id*/
        /* Implement RemoveNews method to remove the existing news*/

        // Throw your own custom Exception whereever its required in AddNews,GetAllNews,GetNewsById and RemoveNews 
        // functionalities
    }
}
