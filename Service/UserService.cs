using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;
namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e UserService by inheriting IUserService
    public class UserService
    {
        /*
      * UserRepository should  be injected through constructor injection. 
      * Please note that we should not create USerRepository object using the new keyword
      */
        public UserService(IUserRepository userRepository)
        {
           
        }
        // UserService class is used to implement all the functionalities declared in the interface

        /* Implement all the methods of respective interface asynchronously*/

        // Implement AddUser method which should be used to save a new user.   
        // Implement DeleteUser method which should be used to delete an existing user.
        // Implement GetUser method which should be used to get a userprofile complete detail by userId.
        // Implement UpdateUser method which should be used to update an existing user.
        // Throw your own custom Exception whereever its required in AddUser,DeleteUser,GetUser and UpdateUser 
        // functionalities
    }
}
