using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        public DateTime Schedule { get; set; }
        public int NewsId { get; set; }
    }
}
