namespace VocabularyManager.UseCases.Exceptions
{
    public class VocabularyNotFoundException : Exception
    {
        public VocabularyNotFoundException(int wordListId) 
            : base($"There is not any word list with id: {wordListId}.")
        {

        }
    }
}
