using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Services.Parsers;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Services
{
    public class OxfordDictionaryServiceTests
    {
        [Fact]
        public async Task Get_List_Of_Words_By_Link_Async_Test_1()
        {
            //Arrange
            var parsingService = new OxfordDictionaryParser();

            //Act
            var wordList = parsingService
                .GetWordListByLinkAsync(
                "https://www.oxfordlearnersdictionaries.com/wordlists/oxford3000-5000",
                new OxfordParsingWordDTOValidator()
            ); //well-fitable url

            //Assert
            Assert.NotNull(wordList);
            Assert.IsAssignableFrom<IAsyncEnumerable<WordDTO>>(wordList);
        }
        [Fact]
        public async Task Get_List_Of_Words_By_Link_Async_Exceptions_Test1()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    null,
                    new OxfordParsingWordDTOValidator()
                );
            });
        }
        [Fact]
        public async Task Get_List_Of_Words_By_Link_Async_Exceptions_Test2()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser();

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    "invalid_url",
                    new OxfordParsingWordDTOValidator()
                );
            }); //fully invalid url, in case when we can't get response as document
        }
        [Fact]
        public async Task Get_List_Of_Words_By_Link_Async_Exceptions_Test3()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser();

            // Act & Assert
            Assert.ThrowsAsync<TheSourceIsNotAppropriateException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    "https://www.oxfordlearnersdictionaries.com/invalid-path",
                    new OxfordParsingWordDTOValidator()
                );
            }); //not appropriate url
        }
    }
}