using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Exceptions
{
    public class NewsNotFoundException:ApplicationException
    {
        public NewsNotFoundException() { }
        public NewsNotFoundException(string message) : base(message) { }
    }
}
