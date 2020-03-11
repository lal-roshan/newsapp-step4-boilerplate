using System;

namespace Service.Exceptions
{
    public class ReminderAlreadyExistsException:ApplicationException
    {
        public ReminderAlreadyExistsException() { }
        public ReminderAlreadyExistsException(string message) : base(message) { }
    }
}
