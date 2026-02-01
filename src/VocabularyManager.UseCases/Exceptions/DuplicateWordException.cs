namespace VocabularyManager.UseCases.Exceptions
{
    public class DuplicateWordException : Exception
    {
        public string WordContent { get; }
        public int VocabularyId { get; }

        public DuplicateWordException(string wordContent, int vocabularyId)
            : base($"A word with content '{wordContent}' already exists in vocabulary {vocabularyId}.")
        {
            WordContent = wordContent;
            VocabularyId = vocabularyId;
        }
    }
}
