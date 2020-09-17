using System;
namespace Entities
{
    /// <summary>
    /// This class will be acting as the data model for the User Table in the database.
    /// </summary>
    public class UserProfile
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
