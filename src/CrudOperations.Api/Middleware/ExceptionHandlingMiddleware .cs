namespace CrudOperations.Api.Middleware
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly ExceptionHandler _exceptionHandler;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger,
            ExceptionHandler exceptionHandler)
        {
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Invokes the custom middleware to handle exceptions during HTTP
        /// request execution by setting the appropriate status code and error details.
        /// </summary>
        /// <param name="context">The HTTP context representing the current request and response.</param>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await _exceptionHandler.HandleExceptionAsync(context, ex);
            }
        }
    }
}