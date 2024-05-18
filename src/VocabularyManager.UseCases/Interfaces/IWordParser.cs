using FluentValidation;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IWordParser<T>
    {
        public IAsyncEnumerable<T> GetWordListByLinkAsync(string link, IValidator<T> validationFilter);
    }
}
