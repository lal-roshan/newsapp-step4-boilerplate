using Entities;
using System;
using System.Threading.Tasks;
namespace DAL
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e UserRepository by inheriting IUserRepository

    // UserRepository class is used to implement all Data access operations
    public class UserRepository
    {
        private readonly NewsDbContext context;
        public UserRepository(NewsDbContext dbContext)
        {
           
        }
        // Implement AddUser method which should be used to save a new user.
        // Implement DeleteUser method which should be used to delete an existing user.
        // Implement GetUser method which should be used to get a userprofile complete detail by userId.
        // Implement UpdateUser method which should be used to update an existing user.
    }
}
