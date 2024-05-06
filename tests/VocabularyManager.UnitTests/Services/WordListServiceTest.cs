using Ardalis.Specification;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Core.Services;
using VocabularyManager.Core.Specifications;
using VocabularyManager.Infrastructure.Data;

namespace VocabularyManager.UnitTests.Services
{
    public class WordListServiceTest
    {
        [Fact]
        public async Task Add_Word_To_Word_List_Test1()
        {
            //Arrange
            WordList wordList = new WordList("some word list", null);
            Word word = new Word("word", "noun", "a1", null);
            int wordListId = 0;
            IRepositoryBase<WordList> repository = Substitute.For<IRepositoryBase<WordList>>();
            repository
                .GetByIdAsync(wordListId)
                .Returns(
                    Task.Run(
                        () => new WordList(wordList.ListName, wordList.SourceUrl)
                        { Id = wordListId, Words = new List<Word>() })
            ); ;
            IWordListService wordListService = new WordListService(repository);

            //Act
            int? wordId = await wordListService.AddWordToWordList(word, wordListId);

            //Assert
            Assert.IsType<int>(wordId);
            Assert.NotNull(wordId);
        }
        [Fact]
        public async Task Create_Word_List_Test1()
        {
            // Arrange
            WordList wordList = new WordList("some word list", null);
            int expectedId = 0;
            IRepositoryBase<WordList> repository = Substitute.For<IRepositoryBase<WordList>>();
            repository
                .AddAsync(wordList)
                .Returns(
                    Task.Run(
                        () => new WordList(wordList.ListName, wordList.SourceUrl) { Id = expectedId })
            );
            IWordListService wordListService = new WordListService(repository);

            //Act
            int? wordListsId = await wordListService.CreateWordList(wordList);

            //Assert
            Assert.IsType<int>(wordListsId);
            Assert.NotNull(wordListsId);
        }
        [Fact]
        public async Task Get_Word_Lists_Test1()
        {
            //Arrange
            List<WordList> wordLists = new List<WordList>();
            wordLists.Add(new WordList("some word list", null));
            IRepositoryBase<WordList> repository = Substitute.For<IRepositoryBase<WordList>>();
            repository
                .ListAsync()
                .Returns(
                    Task.Run(
                        () => wordLists)
            );
            IWordListService wordListService = new WordListService(repository);
            //Act
            var result = await wordListService.GetWordLists();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<WordList>>(wordLists);
            Assert.NotNull(wordLists);
        }
        [Fact]
        public async Task Get_Word_List_By_Id_Test1()
        {
            //Arrange
            WordList expectedWordList = new WordList("some word list", null);
            int wordListId = 0;
            ISpecification<WordList> spec = new WordListWithWordsSpecification(wordListId);
            IRepositoryBase<WordList> repository = Substitute.For<IRepositoryBase<WordList>>();
            repository
                .FirstOrDefaultAsync(
                    Arg.Any<ISpecification<WordList>>()
                )
                .Returns(
                    Task.Run(
                        () => new WordList(expectedWordList.ListName, expectedWordList.SourceUrl)
                        { Id = wordListId, Words = new List<Word>() })
            );
            IWordListService wordListService = new WordListService(repository);

            //Act
            WordList wordList = await wordListService.GetWordListByIdWithWords(wordListId);

            //Assert
            Assert.IsAssignableFrom<WordList>(wordList);
            Assert.NotNull(wordList);
            Assert.NotNull(wordList.Words);
        }
    }
}
