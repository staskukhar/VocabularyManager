using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IWordStorageManager
    {
        Task<Word> GetWordById(int wordId);

        Task<Word> GetGlobalWord(int wordId);

        Task<int> DeleteWordById(int wordId);

        Task UpdateWord(Word word);
    }
}
