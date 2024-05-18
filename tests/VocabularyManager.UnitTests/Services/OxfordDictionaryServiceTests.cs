using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Services;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Services
{
    public class OxfordDictionaryServiceTests
    {
        [Fact]
        public async Task Get_Word_List_By_Link_Async_Test_1()
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
        public void Get_Word_List_By_Link_Async__Exceptions_Test()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    null,
                    new OxfordParsingWordDTOValidator()
                ); // passed url is null value
            });

            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    "invalid-url",
                    new OxfordParsingWordDTOValidator()
                ); // fully invalid url, in case when we can't get response as document
            });

            Assert.ThrowsAsync<TheSourceIsNotAppropriateException>(async () =>
            {
                parsingService.GetWordListByLinkAsync(
                    "https://www.oxfordlearnersdictionaries.com/invalid-path",
                    new OxfordParsingWordDTOValidator()
                ); // not appropriate url
            });
        }
    }
}