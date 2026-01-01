namespace VocabularyManager.UseCases.Exceptions
{
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException(int wordId) : base($"There is not any word with id: {wordId}.")
        {
            
        }
    }
}
