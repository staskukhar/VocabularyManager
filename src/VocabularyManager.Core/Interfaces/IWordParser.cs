namespace VocabularyManager.Core.Interfaces
{
    public interface IWordParser<T>
    {
        public Task<IEnumerable<T>> GetWordListByLinkAsync(string link, Func<T, bool> isValid);
    }
}
