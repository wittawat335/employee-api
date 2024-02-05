using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace sample_api_mongodb.Core.Exceptions
{
    public class GeneralExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> _logger;

        public GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Something went wrong..");

            httpContext.Response.StatusCode = 501;
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync($"Exception Thrown. {exception.Message}", cancellationToken);

            return true;
        }
    }
}
