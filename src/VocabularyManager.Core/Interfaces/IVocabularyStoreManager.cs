using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Interfaces
{
    public interface IVocabularyStoreManager
    {
        Task<int> AddWord(Word word, int wordListId);
        Task AddWords(IEnumerable<Word> words, int wordListId);
        Task<int> CreateVocabulary(Vocabulary wordList);
        Task<IEnumerable<Vocabulary>> GetVocabularies();
        Task<Vocabulary> GetVocabularyWithWordsById(int wordListId);
        Task UpdateVocabulary(Vocabulary wordList);
    }
}
