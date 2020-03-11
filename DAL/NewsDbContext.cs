using Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    //Inherit DbContext class and use Entity Framework Code First Approach
    public class NewsDbContext
    {
        //Create a Constructor and write a logic for Datbase created
        
        /*
        This class should be used as DbContext to speak to database and should make the use of 
        Code First Approach. It should autogenerate the database based upon the model class in 
        your application
        */

        //Create a Dbset for News,USerProfile and Reminders

        /*Override OnModelCreating function to configure relationship between entities and initialize*/

        //write a modelbuilder logic for Relationship between News and UserProfile in OnModelCreating Method
        //write a modelbuilder logic for Relationship between News and Reminder in OnModelCreating Method
    
    }
}
