using System.Net;
using VocabularyManager.Api.Models.Responses;
using VocabularyManager.UseCases.Exceptions;

namespace VocabularyManager.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _nextMiddleware;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger,
            RequestDelegate nextMiddleware
        )
        {
            _logger = logger;
            _nextMiddleware = nextMiddleware;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _nextMiddleware(httpContext);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow} - excpetion handled: {e.Message}");
            HttpStatusCode statusCode = DefineStatusCode(e);
            httpContext.Response.StatusCode = (int) statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                new ExceptionResponse(statusCode, e.Message)
                );
        }
        private HttpStatusCode DefineStatusCode(Exception e)
        {
            switch(e)
            {
                case WordNotFoundException:
                case VocabularyNotFoundException:
                    return HttpStatusCode.BadRequest;
                default: 
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
