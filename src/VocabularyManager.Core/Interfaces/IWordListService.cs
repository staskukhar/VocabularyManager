using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Interfaces
{
    public interface IWordListService
    {
        Task<int> AddWordToWordList(Word word, int wordListId);
        Task AddWordsToWordList(IEnumerable<Word> words, int wordListId);
        Task<int> CreateWordList(WordList wordList);
        Task<IEnumerable<WordList>> GetWordLists();
        Task<WordList> GetWordListByIdWithWords(int wordListId);
        Task UpdateWordList(WordList wordList);
    }
}
