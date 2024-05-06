using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Exceptions;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Core.Specifications;

namespace VocabularyManager.Core.Services
{
    public class WordListService : IWordListService
    {
        private readonly IRepositoryBase<WordList> _wordListRepository;
        public WordListService(IRepositoryBase<WordList> wordListService) 
        {
            _wordListRepository = wordListService;
        }

        public async Task AddWordsToWordList(IEnumerable<Word> words, int wordListId)
        {
            WordList? wordList = await _wordListRepository.GetByIdAsync(wordListId);
            if (wordList == null)
            {
                throw new WordListNotFoundException(wordListId);
            }
            wordList.Words.AddRange(words);
            await _wordListRepository.SaveChangesAsync();
        }

        public async Task<int> AddWordToWordList(Word word, int wordListId)
        {
            WordList? wordList = await _wordListRepository.GetByIdAsync(wordListId);
            if (wordList == null)
            {
                throw new WordListNotFoundException(wordListId);
            }
            wordList.Words.Add(word);
            await _wordListRepository.SaveChangesAsync();
            return word.Id;
        }

        public async Task<int> CreateWordList(WordList wordList)
        {
            WordList createdWordList = await _wordListRepository.AddAsync(wordList);
            await _wordListRepository.SaveChangesAsync();
            return createdWordList.Id;
        }

        public async Task<WordList> GetWordListByIdWithWords(int wordListId)
        {
            var spec = new WordListWithWordsSpecification(wordListId);

            WordList? wordList = await _wordListRepository.FirstOrDefaultAsync(spec);
            if (wordList == null)
            {
                throw new WordListNotFoundException(wordListId);
            }
            return wordList;
        }

        public async Task<IEnumerable<WordList>> GetWordLists()
        {
            return await _wordListRepository.ListAsync();
        }

        public async Task UpdateWordList(WordList wordList)
        {
            await _wordListRepository.UpdateAsync(wordList);
            await _wordListRepository.SaveChangesAsync();
        }
    }
}
