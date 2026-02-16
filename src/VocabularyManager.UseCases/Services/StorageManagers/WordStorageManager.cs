using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Specifications;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.UseCases.Services.StoreManagers
{
    public class WordStorageManager : IWordStorageManager
    {
        private readonly IRepositoryBase<Word> _wordRepository;
        public WordStorageManager(IRepositoryBase<Word> wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<Word> GetWordById(int wordId)
        {
            WordByIdWithMeaningsSpecification spec = new(wordId);
            Word word = await _wordRepository.FirstOrDefaultAsync(spec) ??
                throw new EntityNotFoundException(nameof(Word), wordId);

            return word;
        }

        public async Task<int> DeleteWordById(int wordId)
        {
            Word? wordToDelete = await _wordRepository.GetByIdAsync(wordId);
            if (wordToDelete is null)
                throw new EntityNotFoundException(nameof(Word), wordId);

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
