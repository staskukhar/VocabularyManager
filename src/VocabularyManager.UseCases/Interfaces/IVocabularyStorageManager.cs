using System.Collections.Immutable;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IVocabularyStorageManager
    {
        Task<int> AddWord(Word word, int wordListId);
        Task<ImmutableList<int>> AddWords(IEnumerable<Word> words, int wordListId);
        Task<int> CreateVocabulary(Vocabulary wordList);
        Task<IEnumerable<Vocabulary>> GetVocabularies();
        Task<Vocabulary> GetVocabularyWithWordsById(int wordListId);
        Task UpdateVocabulary(Vocabulary wordList);
        Task<int> Delete(int vocabularyId);
    }
}
