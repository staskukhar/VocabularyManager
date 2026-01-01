using System.Net;

namespace VocabularyManager.Api.Models.Responses
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Message);
}
