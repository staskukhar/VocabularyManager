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
        private readonly IRepositoryBase<Word> _wordRepository;

        public VocabularyStorageManager(
            IRepositoryBase<Vocabulary> vocabularyRepository,
            IRepositoryBase<Word> wordRepository)
        {
            _vocabularyRepository = vocabularyRepository;
            _wordRepository = wordRepository;
        }

        public async Task<ImmutableList<int>> AddWords(IEnumerable<Word> words, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null)
            {
                throw new EntityNotFoundException(nameof(Vocabulary), vocabularyId);
            }

            // Filter out duplicates - words that already exist in the vocabulary
            var wordContents = words.Select(w => w.WordContent).ToList();
            var existingWordsSpec = new WordsByContentsAndVocabularySpecification(wordContents, vocabularyId);
            var existingWords = await _wordRepository.ListAsync(existingWordsSpec);
            var existingWordContents = existingWords.Select(w => w.WordContent).ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Also filter out duplicates within the input itself
            var uniqueNewWords = words
                .Where(w => !existingWordContents.Contains(w.WordContent))
                .GroupBy(w => w.WordContent, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();

            if (uniqueNewWords.Count == 0)
            {
                return ImmutableList<int>.Empty;
            }

            vocabulary.Words.AddRange(uniqueNewWords);
            await _vocabularyRepository.SaveChangesAsync();
            return uniqueNewWords.Select(w => w.Id).ToImmutableList();
        }

        public async Task<int> AddWord(Word word, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null)
            {
                throw new EntityNotFoundException(nameof(Vocabulary), vocabularyId);
            }

            // Check if word already exists in this vocabulary
            var spec = new WordByContentAndVocabularySpecification(word.WordContent, vocabularyId);
            var existingWord = await _wordRepository.FirstOrDefaultAsync(spec);
            if (existingWord != null)
            {
                throw new DuplicateWordException(word.WordContent, vocabularyId);
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
                throw new EntityNotFoundException(nameof(Vocabulary), vocabularyId);
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
                throw new EntityNotFoundException(nameof(Vocabulary), vocabularyId);
            }
            await _vocabularyRepository.DeleteAsync(vocabulary);
            await _vocabularyRepository.SaveChangesAsync();
            return vocabularyId;
        }
    }
}
