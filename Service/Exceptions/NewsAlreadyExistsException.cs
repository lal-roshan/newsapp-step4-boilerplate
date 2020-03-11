using System;

namespace Service.Exceptions
{
    public class NewsAlreadyExistsException:ApplicationException
    {
        public NewsAlreadyExistsException() { }
        public NewsAlreadyExistsException(string message) : base(message) { }
    }
}
