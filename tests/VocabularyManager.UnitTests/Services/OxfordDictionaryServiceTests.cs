using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Services.Parsers;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Services
{
    public class OxfordDictionaryServiceTests
    {
        [Fact]
        public void Get_List_Of_Words_By_Link_Async_Test_1()
        {
            //Arrange
            var parsingService = new OxfordDictionaryParser(new OxfordParsingWordDTOValidator());

            //Act
            var wordList = parsingService
                .GetWordListByLinkAsync(
                "https://www.oxfordlearnersdictionaries.com/wordlists/oxford3000-5000"
            ); //well-fitable url

            //Assert
            wordList
                .Should().NotBeNull()
                .And.BeAssignableTo<IAsyncEnumerable<WordDTO>>();
        }
        [Fact]
        public void Get_List_Of_Words_By_Link_Async_Exceptions_Test1()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser(new OxfordParsingWordDTOValidator());

            // Act & Assert
            FluentActions.Invoking(async () =>
                parsingService.GetWordListByLinkAsync(null!)
            ).Should().ThrowAsync<ArgumentNullException>();
        }
        [Fact]
        public void Get_List_Of_Words_By_Link_Async_Exceptions_Test2()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser(new OxfordParsingWordDTOValidator());

            // Act & Assert
            FluentActions.Invoking(async () =>
                parsingService.GetWordListByLinkAsync(String.Empty)
            ).Should().ThrowAsync<ArgumentNullException>();
            //fully invalid url, in case when we can't get response as document
        }
        [Fact]
        public void Get_List_Of_Words_By_Link_Async_Exceptions_Test3()
        {
            // Arrange
            var parsingService = new OxfordDictionaryParser(new OxfordParsingWordDTOValidator());

            // Act & Assert
            FluentActions.Invoking(async () => 
                parsingService.GetWordListByLinkAsync(
                    "http://oxforddictionary/list/invalid-path"
                )
            ).Should().ThrowAsync<TheSourceIsNotAppropriateException>();
            //not appropriate url
        }
    }
}