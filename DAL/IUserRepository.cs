using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    /*
	 * Should not modify this interface. You have to implement these methods of interface 
     * in corresponding Implementation classes
	 */
    public interface IUserRepository
    {
        Task<bool> AddUser(UserProfile user);
        Task<UserProfile> GetUser(string userId);
        Task<bool> UpdateUser(UserProfile user);
        Task<bool> DeleteUser(UserProfile user);
    }
}
