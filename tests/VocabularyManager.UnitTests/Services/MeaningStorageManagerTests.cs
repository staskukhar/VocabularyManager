using Ardalis.Specification;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.StoreManagers;

namespace VocabularyManager.UnitTests.Services
{
    public class MeaningStorageManagerTests
    {
        private readonly Fixture _fixture;
        private readonly IRepositoryBase<Meaning> _meaningRepository;
        private readonly IWordRepository _wordRepository;
        private readonly MeaningStorageManager _meaningStorageManager;

        public MeaningStorageManagerTests()
        {
            _fixture = new Fixture();
            _meaningRepository = Substitute.For<IRepositoryBase<Meaning>>();
            _wordRepository = Substitute.For<IWordRepository>();
            _meaningStorageManager = new MeaningStorageManager(_meaningRepository, _wordRepository);
        }

        [Fact]
        public async Task AddMeanings_WhenAllMeaningsAreNew_ShouldAddAll()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Word existingWord = new Word("test") { Id = wordId, Meanings = new List<Meaning>() };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> newMeanings = new List<Meaning>
            {
                new Meaning("noun", "a definition", "A1"),
                new Meaning("verb", "another definition", "B2")
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(newMeanings, wordId);

            // Assert
            result.Should().HaveCount(2);
            await _meaningRepository.Received(2).AddAsync(Arg.Any<Meaning>());
            await _meaningRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task AddMeanings_WhenAllMeaningsAreDuplicates_ShouldSkipAll()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Meaning existingMeaning = new Meaning("noun", "a definition", "A1") { WordId = wordId };
            Word existingWord = new Word("test")
            {
                Id = wordId,
                Meanings = new List<Meaning> { existingMeaning }
            };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> duplicateMeanings = new List<Meaning>
            {
                new Meaning("noun", "a definition", "A1")
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(duplicateMeanings, wordId);

            // Assert
            result.Should().BeEmpty();
            await _meaningRepository.DidNotReceive().AddAsync(Arg.Any<Meaning>());
        }

        [Fact]
        public async Task AddMeanings_WhenMixOfNewAndDuplicate_ShouldAddOnlyNew()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Meaning existingMeaning = new Meaning("noun", "existing definition", "A1") { WordId = wordId };
            Word existingWord = new Word("test")
            {
                Id = wordId,
                Meanings = new List<Meaning> { existingMeaning }
            };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> meanings = new List<Meaning>
            {
                new Meaning("noun", "existing definition", "A1"),
                new Meaning("verb", "new definition", "B2")
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(meanings, wordId);

            // Assert
            result.Should().HaveCount(1);
            await _meaningRepository.Received(1).AddAsync(Arg.Any<Meaning>());
        }

        [Fact]
        public async Task AddMeanings_WhenInputContainsDuplicatesWithinBatch_ShouldAddOnlyFirst()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Word existingWord = new Word("test") { Id = wordId, Meanings = new List<Meaning>() };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> meanings = new List<Meaning>
            {
                new Meaning("noun", "same definition", "A1"),
                new Meaning("noun", "same definition", "A1")
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(meanings, wordId);

            // Assert
            result.Should().HaveCount(1);
            await _meaningRepository.Received(1).AddAsync(Arg.Any<Meaning>());
        }

        [Fact]
        public async Task AddMeanings_WhenDifferentLexemeType_ShouldNotBeTreatedAsDuplicate()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Meaning existingMeaning = new Meaning("noun", "a definition", "A1") { WordId = wordId };
            Word existingWord = new Word("test")
            {
                Id = wordId,
                Meanings = new List<Meaning> { existingMeaning }
            };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> meanings = new List<Meaning>
            {
                new Meaning("verb", "a definition", "A1")
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(meanings, wordId);

            // Assert
            result.Should().HaveCount(1);
            await _meaningRepository.Received(1).AddAsync(Arg.Any<Meaning>());
        }

        [Fact]
        public async Task AddMeanings_WhenNullableFieldsMatch_ShouldBeTreatedAsDuplicate()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            Meaning existingMeaning = new Meaning(null, null, null) { WordId = wordId };
            Word existingWord = new Word("test")
            {
                Id = wordId,
                Meanings = new List<Meaning> { existingMeaning }
            };

            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(existingWord));

            List<Meaning> meanings = new List<Meaning>
            {
                new Meaning(null, null, null)
            };

            // Act
            IEnumerable<int> result = await _meaningStorageManager.AddMeanings(meanings, wordId);

            // Assert
            result.Should().BeEmpty();
            await _meaningRepository.DidNotReceive().AddAsync(Arg.Any<Meaning>());
        }

        [Fact]
        public async Task AddMeanings_WhenWordDoesNotExist_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            int wordId = _fixture.Create<int>();
            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(null));

            List<Meaning> meanings = new List<Meaning>
            {
                new Meaning("noun", "a definition", "A1")
            };

            // Act
            Func<Task> action = async () => await _meaningStorageManager.AddMeanings(meanings, wordId);

            // Assert
            await action.Should().ThrowAsync<EntityNotFoundException>();
        }
    }
}
