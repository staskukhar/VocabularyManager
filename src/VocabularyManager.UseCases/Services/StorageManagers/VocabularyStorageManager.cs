using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.Core.Specifications;
using System.Collections.Immutable;

namespace VocabularyManager.UseCases.Services.StoreManagers
{
    public class VocabularyStorageManager : IVocabularyStorageManager
    {
        private readonly IRepositoryBase<Vocabulary> _vocabularyRepository;
        public VocabularyStorageManager(IRepositoryBase<Vocabulary> vocabularyRepository)
        {
            _vocabularyRepository = vocabularyRepository;
        }

        public async Task<ImmutableList<int>> AddWords(IEnumerable<Word> words, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null)
            {
                throw new VocabularyNotFoundException(vocabularyId);
            }
            vocabulary.Words.AddRange(words);
            await _vocabularyRepository.SaveChangesAsync();
            return words.Select(w => w.Id).ToImmutableList();
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

        public async Task<int> Delete(int vocabularyId)
        {
            var spec = new VocabularyWithWordsSpecification(vocabularyId);

            Vocabulary? vocabulary = await _vocabularyRepository.FirstOrDefaultAsync(spec);
            if (vocabulary == null)
            {
                throw new VocabularyNotFoundException(vocabularyId);
            }
            await _vocabularyRepository.DeleteAsync(vocabulary);
            await _vocabularyRepository.SaveChangesAsync();
            return vocabularyId;
        }
    }
}
