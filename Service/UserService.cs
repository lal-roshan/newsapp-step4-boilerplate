using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// This class is used to implement all input validation operations for User CRUD operations
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// readonly property for repository
        /// </summary>
        readonly IUserRepository repository;

        /// <summary>
        /// Parametrised constructor for injecting repository
        /// </summary>
        /// <param name="repository"></param>
        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method for adding new user
        /// </summary>
        /// <param name="user">The user profile that is to be added</param>
        /// <returns>True if user was added successfully</returns>
        public async Task<bool> AddUser(UserProfile user)
        {
            UserProfile presentUser = await repository.GetUser(user.UserId);
            if (presentUser == null)
            {
                return await repository.AddUser(user);
            }
            throw new UserAlreadyExistsException($"{user.UserId} already exists");
        }

        /// <summary>
        /// Method to delete a user
        /// </summary>
        /// <param name="userId">Id of the user to be deleted</param>
        /// <returns>Returns true if the user was removed</returns>
        public async Task<bool> DeleteUser(string userId)
        {
            UserProfile user = await repository.GetUser(userId);
            if (user != null)
            {
                return await repository.DeleteUser(user);
            }
            throw new UserNotFoundException($"{userId} doesn't exist");
        }

        /// <summary>
        /// Method to fetch details of a user
        /// </summary>
        /// <param name="userId">The id of the user whose details are to be fetched</param>
        /// <returns>The user profile of the user with mentioned id</returns>
        public async Task<UserProfile> GetUser(string userId)
        {
            UserProfile user = await repository.GetUser(userId);
            if (user != null)
            {
                return user;
            }
            throw new UserNotFoundException($"{userId} doesn't exist");
        }

        /// <summary>
        /// Method for updating details of a user
        /// </summary>
        /// <param name="userId">The id of the user to be updated</param>
        /// <param name="user">The details to be updated to the user</param>
        /// <returns>True if user was updated</returns>
        public async Task<bool> UpdateUser(string userId, UserProfile user)
        {
            bool updated = await repository.UpdateUser(user);
            if (updated)
            {
                return updated;
            }
            throw new UserNotFoundException($"{userId} doesn't exist");
        }
    }
}
