using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace DAL
{
    /// <summary>
    /// Class to fascilitate CRUD operations on Users table
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// readonly property for dbconext
        /// </summary>
        readonly NewsDbContext dbContext;

        /// <summary>
        /// Parametrised constructor for injecting dbcontext property
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(NewsDbContext dbContext)
        {
            this.dbContext = dbContext;
            // Implement AddUser method which should be used to save a new user.   
            // Implement DeleteUser method which should be used to delete an existing user.
            // Implement GetUser method which should be used to get a userprofile complete detail by userId.
            // Implement UpdateUser method which should be used to update an existing user.
        }

        /// <summary>
        /// Method to add a user
        /// </summary>
        /// <param name="user">The user object that is to be added</param>
        /// <returns>True if user added successfuly</returns>
        public async Task<bool> AddUser(UserProfile user)
        {
            await dbContext.Users.AddAsync(user);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Method to delete a user
        /// </summary>
        /// <param name="user">The user object that is to be deleted</param>
        /// <returns>True if user deleted successfuly</returns>
        public async Task<bool> DeleteUser(UserProfile user)
        {
            dbContext.Users.Remove(user);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Method to fetch details of a user by user Id
        /// </summary>
        /// <param name="userId">The id of the user whose details is to be fetched</param>
        /// <returns>The user profile corresponding to the id mentioned</returns>
        public async Task<UserProfile> GetUser(string userId)
        {
            return await dbContext.Users.Where(user => string.Equals(user.UserId, userId)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to update properties of a user
        /// </summary>
        /// <param name="user">The user object whose properties are to be updated</param>
        /// <returns>True if updation successful</returns>
        public async Task<bool> UpdateUser(UserProfile user)
        {
            var presentUser = await GetUser(user.UserId);
            if (presentUser != null)
            {
                presentUser.FirstName = user.FirstName;
                presentUser.LastName = user.LastName;
                presentUser.Contact = user.Contact;
                presentUser.Email = user.Email;
                presentUser.CreatedAt = user.CreatedAt;
            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
