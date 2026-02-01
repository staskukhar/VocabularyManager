using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyManager.UseCases.Exceptions;

namespace VocabularyManager.Api.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = CreateProblemDetails(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(Exception exception)
    {
        return exception switch
        {
            EntityNotFoundException entityNotFound => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource Not Found",
                Detail = entityNotFound.Message,
            },
            DuplicateWordException duplicateWord => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Duplicate Resource",
                Detail = duplicateWord.Message,
            },
            DbUpdateException dbUpdateException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "DB operation exception",
                Detail = dbUpdateException.Message,
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
            }
        };
    }
}
