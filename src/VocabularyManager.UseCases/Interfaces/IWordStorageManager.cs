using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IWordStorageManager
    {
        Task<int> DeleteWordById(int wordId);
        Task UpdateWord(Word word);
    }
}
