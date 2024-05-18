using Ardalis.Specification;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Core.Services;
using VocabularyManager.Core.Specifications;

namespace VocabularyManager.UnitTests.Services
{
    public class VocabularyStoreManagerTest
    {
        [Fact]
        public async Task Add_Word_To_Word_List_Test1()
        {
            //Arrange
            Vocabulary vocabulary = new Vocabulary("some word list", null);
            Word word = new Word("word", "noun", "a1", null);
            int vocabularyId = 0;
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .GetByIdAsync(vocabularyId)
                .Returns(
                    Task.Run(
                        () => new Vocabulary(vocabulary.Name, vocabulary.SourceUrl)
                        { Id = vocabularyId, Words = new List<Word>() })
            ); ;
            IVocabularyStoreManager vocabularyStoreService = new VocabularyStoreManager(repository);

            //Act
            int? wordId = await vocabularyStoreService.AddWord(word, vocabularyId);

            //Assert
            Assert.IsType<int>(wordId);
            Assert.NotNull(wordId);
        }
        [Fact]
        public async Task Create_Word_List_Test1()
        {
            // Arrange
            Vocabulary vocabulary = new Vocabulary("some word list", null);
            int expectedId = 0;
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .AddAsync(vocabulary)
                .Returns(
                    Task.Run(
                        () => new Vocabulary(vocabulary.Name, vocabulary.SourceUrl) { Id = expectedId })
            );
            IVocabularyStoreManager vocabularyStoreService = new VocabularyStoreManager(repository);

            //Act
            int? vocabularyId = await vocabularyStoreService.CreateVocabulary(vocabulary);

            //Assert
            Assert.IsType<int>(vocabularyId);
            Assert.NotNull(vocabularyId);
        }
        [Fact]
        public async Task Get_Word_Lists_Test1()
        {
            //Arrange
            List<Vocabulary> vocabulary = new List<Vocabulary>();
            vocabulary.Add(new Vocabulary("some word list", null));
            IRepositoryBase<Vocabulary> repository = Substitute.For<IRepositoryBase<Vocabulary>>();
            repository
                .ListAsync()
                .Returns(
                    Task.Run(
                        () => vocabulary)
            );
            IVocabularyStoreManager vocabularyStoreService = new VocabularyStoreManager(repository);
            //Act
            var result = await vocabularyStoreService.GetVocabularies();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Vocabulary>>(vocabulary);
            Assert.NotNull(vocabulary);
        }
        [Fact]
        public async Task Get_Word_List_By_Id_Test1()
        {
            //Arrange
            Vocabulary expectedVocabulary = new Vocabulary("some word list", null);
            int vocabularyId = 0;
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
            IVocabularyStoreManager vocabularyStoreService = new VocabularyStoreManager(repository);

            //Act
            Vocabulary wordList = await vocabularyStoreService.GetVocabularyWithWordsById(vocabularyId);

            //Assert
            Assert.IsAssignableFrom<Vocabulary>(wordList);
            Assert.NotNull(wordList);
            Assert.NotNull(wordList.Words);
        }
    }
}
