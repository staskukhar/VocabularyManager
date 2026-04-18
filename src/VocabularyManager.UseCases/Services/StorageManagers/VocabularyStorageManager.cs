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
        private readonly IMeaningStorageManager _meaningStorageManager;

        private readonly IRepositoryBase<Vocabulary> _vocabularyRepository;

        private readonly IWordRepository _wordRepository;

        public VocabularyStorageManager(
            IMeaningStorageManager meaningStorageManager,
            IRepositoryBase<Vocabulary> vocabularyRepository,
            IWordRepository wordRepository)
        {
            _meaningStorageManager = meaningStorageManager;
            _vocabularyRepository = vocabularyRepository;
            _wordRepository = wordRepository;
        }

        public async Task<ImmutableList<int>> AddWords(IEnumerable<Word> words, int vocabularyId)
        {
            Vocabulary? vocabulary = await _vocabularyRepository.FirstOrDefaultAsync(
                new VocabularyWithWordsSpecification(vocabularyId));
            if (vocabulary == null)
            {
                throw new EntityNotFoundException(nameof(Vocabulary), vocabularyId);
            }

            List<Word> newWords = [];
            foreach (Word word in words)
            {
                Word? existingWord = vocabulary.Words.FirstOrDefault(w => w.WordContent == word.WordContent && w.VocabularyId == vocabularyId);
                if (existingWord != null)
                {
                    await _meaningStorageManager.AddMeanings(word.Meanings, existingWord.Id);
                }
                else
                {
                    word.VocabularyId = vocabularyId;
                    newWords.Add(
                        await _wordRepository.AddAsync(word));
                }
            }

            await _vocabularyRepository.SaveChangesAsync();
            return newWords.Select(w => w.Id).ToImmutableList();
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

        public async Task<IEnumerable<Vocabulary>> GetVocabularies(bool withWords)
        {
            return withWords ?
                await _vocabularyRepository.ListAsync(new VocabularyWithWordsSpecification()) :
                await _vocabularyRepository.ListAsync();
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
