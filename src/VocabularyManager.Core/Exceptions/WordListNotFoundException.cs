namespace VocabularyManager.Core.Exceptions
{
    public class WordListNotFoundException : Exception
    {
        public WordListNotFoundException(int wordListId) 
            : base($"There is not any word list with id: {wordListId}.")
        {

        }
    }
}
