using DictionaryParser.Services;
using VocabularyManager.Core.Entities;
using VocabularyManager.Infrastructure.Exceptions;
using VocabularyManager.Infrastructure.Validators;

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
            var wordList = await parsingService
                .GetWordListByLinkAsync(
                "https://www.oxfordlearnersdictionaries.com/wordlists/oxford3000-5000",
                OxfordDictionaryValidator.IsWordObjectDataValid
            ); //well-fitable url

            //Assert
            Assert.NotNull(wordList);
            Assert.IsAssignableFrom<IEnumerable<Word>>(wordList);
        }

        [Fact]
        public void Get_Word_List_By_Link_Async__Exceptions_Test()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await parsingService.GetWordListByLinkAsync(
                    null,
                    OxfordDictionaryValidator.IsWordObjectDataValid
                ); // passed url is null value
            });

            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await parsingService.GetWordListByLinkAsync(
                    "invalid-url",
                    OxfordDictionaryValidator.IsWordObjectDataValid
                ); // fully invalid url, in case when we can't get response as document
            });

            Assert.ThrowsAsync<TheSourceIsNotAppropriateException>(async () =>
            {
                await parsingService.GetWordListByLinkAsync(
                    "https://www.oxfordlearnersdictionaries.com/invalid-path",
                    OxfordDictionaryValidator.IsWordObjectDataValid
                ); // not appropriate url
            });
        }
    }
}