using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IMeaningStorageManager
    {
        Task<int> AddMeaning(Meaning meaning, int wordId);
        Task<int> DeleteMeaningById(int meaningId);
        Task UpdateMeaning(Meaning meaning);
    }
}
