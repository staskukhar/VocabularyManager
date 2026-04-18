using Ardalis.Specification;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Specifications;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.StoreManagers;

namespace VocabularyManager.UnitTests.Services
{
    public class VocabularyStoreManagerTest
    {
        private readonly Fixture _fixture;
        private readonly IWordRepository _wordRepository;

        public VocabularyStoreManagerTest()
        {
            _fixture = new Fixture();
            _wordRepository = Substitute.For<IWordRepository>();
        }

        [Fact]
        public async Task Add_Word_To_Vocabulary_Test1()
        {
            //Arrange
            Vocabulary vocabulary = new Vocabulary(_fixture.Create<string>(), null);
            Word word = new Word(_fixture.Create<string>());
            int vocabularyId = _fixture.Create<int>();
            IMeaningStorageManager meaningStorageManager = Substitute.For<IMeaningStorageManager>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .GetByIdAsync(vocabularyId)
                .Returns(
                    Task.FromResult<Vocabulary?>(new Vocabulary(vocabulary.Name, vocabulary.SourceUrl)
                        { Id = vocabularyId, Words = new List<Word>() })
            );
            // Return empty list to indicate no duplicates
            _wordRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Word>>())
                .Returns(Task.FromResult<Word?>(null));

            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(meaningStorageManager, repository, _wordRepository);

            //Act
            int? wordId = await vocabularyStoreService.AddWord(word, vocabularyId);

            //Assert
            wordId
                .Should().NotBeNull()
                .And.BeOfType(typeof(int));
        }

        [Fact]
        public async Task Create_Vocabulary_Test1()
        {
            // Arrange
            Vocabulary vocabulary = new Vocabulary(_fixture.Create<string>(), null);
            int expectedId = _fixture.Create<int>();
            IMeaningStorageManager meaningStorageManager = Substitute.For<IMeaningStorageManager>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .AddAsync(vocabulary)
                .Returns(
                    Task.FromResult(new Vocabulary(vocabulary.Name, vocabulary.SourceUrl) { Id = expectedId })
            );
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(meaningStorageManager, repository, _wordRepository);

            //Act
            int? vocabularyId = await vocabularyStoreService.CreateVocabulary(vocabulary);

            //Assert
            vocabularyId
                .Should().NotBeNull()
                .And.BeOfType(typeof(int));
        }

        [Fact]
        public async Task Get_Vocabularies_Test1()
        {
            //Arrange
            List<Vocabulary> vocabulary = new List<Vocabulary>();
            vocabulary.Add(new Vocabulary(_fixture.Create<string>(), null));
            IMeaningStorageManager meaningStorageManager = Substitute.For<IMeaningStorageManager>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .ListAsync()
                .Returns(Task.FromResult(vocabulary));
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(meaningStorageManager, repository, _wordRepository);

            //Act
            var result = await vocabularyStoreService.GetVocabularies(false);

            //Assert
            vocabulary
                .Should().BeAssignableTo<IEnumerable<Vocabulary>>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task Get_Vocabulary_By_Id_Test1()
        {
            //Arrange
            Vocabulary expectedVocabulary = new Vocabulary(_fixture.Create<string>(), null);
            int vocabularyId = _fixture.Create<int>();
            ISpecification<Vocabulary> spec = new VocabularyWithWordsSpecification(vocabularyId);
            IMeaningStorageManager meaningStorageManager = Substitute.For<IMeaningStorageManager>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .FirstOrDefaultAsync(
                    Arg.Any<ISpecification<Vocabulary>>()
                )
                .Returns(
                    Task.FromResult<Vocabulary?>(new Vocabulary(expectedVocabulary.Name, expectedVocabulary.SourceUrl)
                        { Id = vocabularyId, Words = new List<Word>() })
            );
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(meaningStorageManager, repository, _wordRepository);

            //Act
            Vocabulary wordList = await vocabularyStoreService.GetVocabularyWithWordsById(vocabularyId);

            //Assert
            wordList.Words
                .Should().NotBeNull();
            wordList
                .Should().NotBeNull()
                .And.BeOfType<Vocabulary>();
        }
    }
}
