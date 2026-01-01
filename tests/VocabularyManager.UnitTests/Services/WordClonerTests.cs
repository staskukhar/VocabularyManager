using VocabularyManager.BlazorApp.Services;
using VocabularyManager.BlazorApp.Models.Views;
using AutoFixture;
using FluentAssertions;

namespace VocabularyManager.UnitTests.Services
{
    public class WordClonerTests
    {
        Fixture _fixture;
        public WordClonerTests()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void Word_Clone_Test1()
        {
            //Arrange
            var word = new WordView(
                _fixture.Create<string>(),
                _fixture.Create<string?>(),
                _fixture.Create<string?>(),
                _fixture.Create<string?>()
            );

            //Act
            var clonedWord = WordCloner.CloneWord(word);

            //Assert
            clonedWord
                .Should().NotBeSameAs(word);
            clonedWord
                .Should().BeEquivalentTo(word);
        }
    }
}
