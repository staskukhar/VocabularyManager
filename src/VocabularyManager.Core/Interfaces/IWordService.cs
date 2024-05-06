using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Interfaces
{
    public interface IWordService
    {
        Task<int> DeleteWordById(int wordId);
        Task UpdateWord(Word word);
    }
}
