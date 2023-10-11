using LibraryAPI.Domain.Exeptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CrudOperations.Api.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invokes the custom middleware to handle exceptions during HTTP
        /// request execution by setting the appropriate status code and error details.
        /// </summary>
        /// <param name="context">The HTTP context representing the current request and response.</param>
        /// <returns>A task representing the asynchronous middleware operation.</returns>
        /// <exception cref="OrderNotFoundException">Thrown when the requested Order is not found.</exception>
        /// <exception cref="UserNotFoundException">Thrown when the requested User is not found.</exception>
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string title;
            string detail;

            if (ex is BookNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = "Not found error";
                detail = "A not found error has occurred";
            }
            if (ex is BookNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = "Not found error";
                detail = "A not found error has occurred";
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                title = "Server error";
                detail = "An internal server error has occurred";
            }

            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = statusCode;

            ProblemDetails problem = new()
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

            string json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);

            context.Response.ContentType = "application/json";
        }
    }
}