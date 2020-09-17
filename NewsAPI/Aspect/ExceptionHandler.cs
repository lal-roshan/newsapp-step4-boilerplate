using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Service.Exceptions;
using System;
using System.Net;

namespace NewsAPI.Aspect
{
    public class ExceptionHandler : IExceptionFilter
    {
        /// <summary>
        /// Enum containing list of all custom exceptions to be handled
        /// </summary>
        enum Exceptions
        {
            NewsAlreadyExistsException,
            NewsNotFoundException,
            ReminderAlreadyExistsException,
            ReminderNotFoundException,
            UserAlreadyExistsException,
            UserNotFoundException
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception as WebException != null && 
                        ((HttpWebResponse) (context.Exception as WebException).Response) != null) ?  
                         ((HttpWebResponse) (context.Exception as WebException).Response).StatusCode  
                         :getErrorCode(context.Exception.GetType());
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }

        private HttpStatusCode getErrorCode(Type exceptionType)
        {
            Exceptions parseResult;
            if (Enum.TryParse(exceptionType.Name, out parseResult))
            {
                switch (parseResult)
                {
                    case Exceptions.NewsAlreadyExistsException:
                    case Exceptions.ReminderAlreadyExistsException:
                    case Exceptions.UserAlreadyExistsException:
                        return HttpStatusCode.Conflict;
                    case Exceptions.NewsNotFoundException:
                    case Exceptions.ReminderNotFoundException:
                    case Exceptions.UserNotFoundException:
                        return HttpStatusCode.NotFound;
                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            return HttpStatusCode.InternalServerError;
        }
    }
}
