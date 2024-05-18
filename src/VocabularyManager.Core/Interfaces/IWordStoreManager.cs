using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Interfaces
{
    public interface IWordStoreManager
    {
        Task<int> DeleteWordById(int wordId);
        Task UpdateWord(Word word);
    }
}
