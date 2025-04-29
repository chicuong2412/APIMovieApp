using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Exception.Handlers
{
    public class AppExceptionHandler : IExceptionHandler
    {

        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {

            if (exception is not AppException appException)
            {
                return false;
            }
            var problemResponse = new ProblemDetails
            {
                Status = appException.ReturnCode.Code,
                Detail = appException.ReturnCode.Message,
                Title = "An app exception just occured!!"
            };

            _logger.LogError(appException, "Exception occurred: {Message}", appException.Message);

            httpContext.Response.StatusCode = appException.ReturnCode.Code;

            await httpContext.Response.WriteAsJsonAsync(problemResponse, cancellationToken);

            return true;
        }
    }
}
