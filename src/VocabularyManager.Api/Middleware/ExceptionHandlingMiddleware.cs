using System.Net;
using Microsoft.EntityFrameworkCore;
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
            RequestDelegate nextMiddleware)
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
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow} - exception handled: {e.Message}");

            var (statusCode, message) = GetErrorDetails(e);
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                new ExceptionResponse(statusCode, message));
        }

        private (HttpStatusCode statusCode, string message) GetErrorDetails(Exception e)
        {
            return e switch
            {
                WordNotFoundException => (HttpStatusCode.NotFound, e.Message),
                VocabularyNotFoundException => (HttpStatusCode.NotFound, e.Message),
                MeaningNotFoundException => (HttpStatusCode.NotFound, e.Message),
                DuplicateWordException => (HttpStatusCode.Conflict, e.Message),
                DbUpdateException dbEx when IsUniqueConstraintViolation(dbEx) =>
                    (HttpStatusCode.Conflict, "A duplicate entry was detected. The word already exists in this vocabulary."),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };
        }

        private static bool IsUniqueConstraintViolation(DbUpdateException dbEx)
        {
            // PostgreSQL unique constraint violation error code is 23505
            return dbEx.InnerException?.Message?.Contains("23505") == true
                || dbEx.InnerException?.Message?.Contains("duplicate key") == true
                || dbEx.InnerException?.Message?.Contains("unique constraint") == true;
        }
    }
}
