using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.Core.Interfaces;

namespace VocabularyManager.Core.Services
{
    public class WordStoreManager : IWordStoreManager
    {
        private readonly IRepositoryBase<Word> _wordRepository;
        public WordStoreManager(IRepositoryBase<Word> wordRepository) 
        {
            _wordRepository = wordRepository;
        }
        public async Task<int> DeleteWordById(int wordId)
        {
            Word? wordToDelete = await _wordRepository.GetByIdAsync(wordId);
            if (wordToDelete == null)
            {
                throw new WordNotFoundException(wordId);
            }

            await _wordRepository.DeleteAsync(wordToDelete);
            await _wordRepository.SaveChangesAsync();
            return wordToDelete.Id;
        }
        public async Task UpdateWord(Word word)
        {
            await _wordRepository.UpdateAsync(word);
            await _wordRepository.SaveChangesAsync();
        }
    }
}
