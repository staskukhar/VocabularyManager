using Ardalis.Specification;
using AutoFixture;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Specifications;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.StoreManagers;

namespace VocabularyManager.UnitTests.Services
{
    public class VocabularyStoreManagerTest
    {
        Fixture _fixture;
        public VocabularyStoreManagerTest()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public async Task Add_Word_To_Vocabulary_Test1()
        {
            //Arrange
            Vocabulary vocabulary = new Vocabulary(_fixture.Create<string>(), null);
            Word word = new Word(
                _fixture.Create<string>(), 
                _fixture.Create<string>(), 
                _fixture.Create<string>(), 
                null
            );
            int vocabularyId = _fixture.Create<int>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .GetByIdAsync(vocabularyId)
                .Returns(
                    Task.Run(
                        () => new Vocabulary(vocabulary.Name, vocabulary.SourceUrl)
                        { Id = vocabularyId, Words = new List<Word>() })
            ); ;
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(repository);

            //Act
            int? wordId = await vocabularyStoreService.AddWord(word, vocabularyId);

            //Assert
            Assert.IsType<int>(wordId);
            Assert.NotNull(wordId);
        }
        [Fact]
        public async Task Create_Vocabulary_Test1()
        {
            // Arrange
            Vocabulary vocabulary = new Vocabulary(_fixture.Create<string>(), null);
            int expectedId = _fixture.Create<int>();
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .AddAsync(vocabulary)
                .Returns(
                    Task.Run(
                        () => new Vocabulary(vocabulary.Name, vocabulary.SourceUrl) { Id = expectedId })
            );
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(repository);

            //Act
            int? vocabularyId = await vocabularyStoreService.CreateVocabulary(vocabulary);

            //Assert
            Assert.IsType<int>(vocabularyId);
            Assert.NotNull(vocabularyId);
        }
        [Fact]
        public async Task Get_Vocabularies_Test1()
        {
            //Arrange
            List<Vocabulary> vocabulary = new List<Vocabulary>();
            vocabulary.Add(new Vocabulary(_fixture.Create<string>(), null));
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .ListAsync()
                .Returns(
                    Task.Run(
                        () => vocabulary)
            );
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(repository);
            //Act
            var result = await vocabularyStoreService.GetVocabularies();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Vocabulary>>(vocabulary);
            Assert.NotNull(vocabulary);
        }
        [Fact]
        public async Task Get_Vocabulary_By_Id_Test1()
        {
            //Arrange
            Vocabulary expectedVocabulary = new Vocabulary(_fixture.Create<string>(), null);
            int vocabularyId = _fixture.Create<int>();
            ISpecification<Vocabulary> spec = new VocabularyWithWordsSpecification(vocabularyId);
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .FirstOrDefaultAsync(
                    Arg.Any<ISpecification<Vocabulary>>()
                )
                .Returns(
                    Task.Run(
                        () => new Vocabulary(expectedVocabulary.Name, expectedVocabulary.SourceUrl)
                        { Id = vocabularyId, Words = new List<Word>() })
            );
            IVocabularyStorageManager vocabularyStoreService = new VocabularyStorageManager(repository);

            //Act
            Vocabulary wordList = await vocabularyStoreService.GetVocabularyWithWordsById(vocabularyId);

            //Assert
            Assert.IsAssignableFrom<Vocabulary>(wordList);
            Assert.NotNull(wordList);
            Assert.NotNull(wordList.Words);
        }
    }
}
