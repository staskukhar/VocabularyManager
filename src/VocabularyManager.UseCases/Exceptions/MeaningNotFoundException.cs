namespace VocabularyManager.UseCases.Exceptions
{
    public class MeaningNotFoundException : Exception
    {
        public MeaningNotFoundException(int meaningId)
            : base($"Meaning with id: {meaningId} was not found.")
        {
        }
    }
}
