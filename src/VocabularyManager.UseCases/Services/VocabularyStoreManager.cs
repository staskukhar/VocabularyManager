using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Core.Specifications;

namespace VocabularyManager.Core.Services
{
    public class VocabularyStoreManager : IVocabularyStoreManager
    {
        private readonly IRepositoryBase<Vocabulary> _vocabularyRepository;
        public VocabularyStoreManager(IRepositoryBase<Vocabulary> wordListService) 
        {
            _vocabularyRepository = wordListService;
        }

        public async Task AddWords(IEnumerable<Word> words, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null)
            {
                throw new VocabularyNotFoundException(vocabularyId);
            }
            vocabulary.Words.AddRange(words);
            await _vocabularyRepository.SaveChangesAsync();
        }

        public async Task<int> AddWord(Word word, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null)
            {
                throw new VocabularyNotFoundException(vocabularyId);
            }
            vocabulary.Words.Add(word);
            await _vocabularyRepository.SaveChangesAsync();
            return word.Id;
        }

        public async Task<int> CreateVocabulary(Vocabulary vocabulary)
        {
            Vocabulary createdVocabulary = await _vocabularyRepository.AddAsync(vocabulary);
            await _vocabularyRepository.SaveChangesAsync();
            return createdVocabulary.Id;
        }

        public async Task<Vocabulary> GetVocabularyWithWordsById(int vocabularyId)
        {
            var spec = new VocabularyWithWordsSpecification(vocabularyId);

            Vocabulary? vocabulary = await _vocabularyRepository.FirstOrDefaultAsync(spec);
            if (vocabulary == null)
            {
                throw new VocabularyNotFoundException(vocabularyId);
            }
            return vocabulary;
        }

        public async Task<IEnumerable<Vocabulary>> GetVocabularies()
        {
            return await _vocabularyRepository.ListAsync();
        }

        public async Task UpdateVocabulary(Vocabulary vocabulary)
        {
            await _vocabularyRepository.UpdateAsync(vocabulary);
            await _vocabularyRepository.SaveChangesAsync();
        }
    }
}
