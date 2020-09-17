using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        /// <summary>
        /// Method that gets invoked when some exception occurs
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = getErrorCode(context.Exception.GetType());
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.ContentType = "text/plain";
            context.Result = new ObjectResult(context.Exception.Message);
        }

        /// <summary>
        /// Method to find the appropriate http status codes based on exception types
        /// </summary>
        /// <param name="exceptionType">The type of the exception that occured</param>
        /// <returns></returns>
        private HttpStatusCode getErrorCode(Type exceptionType)
        {
            Exceptions parseResult;
            if (Enum.TryParse(exceptionType.Name, out parseResult))
            {
                switch (parseResult)
                {
                    ///If any entity exists already when user is trying to create a Conflict: 409 should be returned
                    case Exceptions.NewsAlreadyExistsException:
                    case Exceptions.ReminderAlreadyExistsException:
                    case Exceptions.UserAlreadyExistsException:
                        return HttpStatusCode.Conflict;
                    ///If any enitity that user tries to access doesnt exists a NotFound: 404 should be returned
                    case Exceptions.NewsNotFoundException:
                    case Exceptions.ReminderNotFoundException:
                    case Exceptions.UserNotFoundException:
                        return HttpStatusCode.NotFound;
                    ///For all other exceptions than custom exceptions return InternalServerError: 500
                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            return HttpStatusCode.InternalServerError;
        }
    }
}
