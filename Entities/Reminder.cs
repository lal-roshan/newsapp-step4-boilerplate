using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// Thi class will be acting as the data model for the Reminder Table in the database.
    /// </summary>
    public class Reminder
    {
        public int ReminderId { get; set; }

        public DateTime Schedule { get; set; }

        public int NewsId { get; set; }
    }
}
