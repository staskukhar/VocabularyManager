using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Specifications;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.UseCases.Services.StoreManagers
{
    public class MeaningStorageManager : IMeaningStorageManager
    {
        private readonly IRepositoryBase<Meaning> _meaningRepository;
        private readonly IWordRepository _wordRepository;

        public MeaningStorageManager(
            IRepositoryBase<Meaning> meaningRepository,
            IWordRepository wordRepository)
        {
            _meaningRepository = meaningRepository;
            _wordRepository = wordRepository;
        }

        public async Task<int> AddMeaning(Meaning meaning, int wordId)
        {
            Word? word = await _wordRepository.GetByIdAsync(wordId);
            if (word == null)
            {
                throw new EntityNotFoundException(nameof(Word), wordId);
            }

            meaning.WordId = wordId;
            await _meaningRepository.AddAsync(meaning);
            await _meaningRepository.SaveChangesAsync();
            return meaning.Id;
        }

        public async Task<IEnumerable<int>> AddMeanings(IEnumerable<Meaning> meanings, int wordId)
        {
            Word? word = await _wordRepository.FirstOrDefaultAsync(
                new WordByIdWithMeaningsSpecification(wordId));
            if (word == null)
            {
                throw new EntityNotFoundException(nameof(Word), wordId);
            }

            IList<int> addedMeaningIds = new List<int>();
            foreach (Meaning meaning in meanings)
            {
                if (IsDuplicateMeaning(word.Meanings, meaning))
                {
                    continue;
                }

                meaning.WordId = wordId;
                await _meaningRepository.AddAsync(meaning);
                addedMeaningIds.Add(meaning.Id);
                word.Meanings.Add(meaning);
            }

            await _meaningRepository.SaveChangesAsync();
            return addedMeaningIds;
        }

        private bool IsDuplicateMeaning(List<Meaning> existingMeanings, Meaning candidate)
        {
            return existingMeanings.Any(existing =>
                existing.Definition == candidate.Definition &&
                existing.LexemeType == candidate.LexemeType &&
                existing.Level == candidate.Level);
        }

        public async Task<int> DeleteMeaningById(int meaningId)
        {
            Meaning? meaningToDelete = await _meaningRepository.GetByIdAsync(meaningId);
            if (meaningToDelete == null)
            {
                throw new EntityNotFoundException(nameof(Meaning), meaningId);
            }

            await _meaningRepository.DeleteAsync(meaningToDelete);
            await _meaningRepository.SaveChangesAsync();
            return meaningToDelete.Id;
        }

        public async Task UpdateMeaning(Meaning meaning)
        {
            Meaning? existingMeaning = await _meaningRepository.GetByIdAsync(meaning.Id);
            if (existingMeaning == null)
            {
                throw new EntityNotFoundException(nameof(Meaning), meaning.Id);
            }

            await _meaningRepository.UpdateAsync(meaning);
            await _meaningRepository.SaveChangesAsync();
        }
    }
}
