using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Middleware
{
    public class LoggingMiddleware
    {
        /// <summary>
        /// Pointer to next middleware
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The path of the log file
        /// </summary>
        string logFilePath;

        /// <summary>
        /// A stopwatch object used to record the time taken for a request to process
        /// </summary>
        Stopwatch stopwatch;

        /// <summary>
        /// Parameterised constructor for initalising the properties
        /// </summary>
        /// <param name="next"></param>
        /// <param name="environment"></param>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            logFilePath = environment.ContentRootPath + @"/LogFile.txt";
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Method that gets invoked when a request is invoked and logs the details
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            ///Reset the stopwatch
            stopwatch.Reset();

            StringBuilder sb = new StringBuilder();
            sb.Append($"Process Incoming Time: {DateTime.Now}");

            ///Start the stopwatch and continue the process using next delegate and
            ///stop the stopwatch once process is over
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();

            sb.Append($"; Processing Time: {stopwatch.Elapsed.TotalSeconds} seconds");
            sb.Append($"; URI: {context.Request.Path}");
            sb.Append($"; Http Verb: {context.Request.Method}");
            sb.Append($"; Status: {context.Response.StatusCode}");
            sb.AppendLine();

            ///Check whether the log files exists or not
            ///if exists append log to the present contents of the file
            ///else write log to the file
            bool append = true;
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Dispose();
                append = false;
            }

            using(StreamWriter sw = new StreamWriter(logFilePath, append))
            {
                await sw.WriteAsync(sb.ToString());
            }
        }
    }
}
