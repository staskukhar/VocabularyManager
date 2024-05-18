using VocabularyManager.BlazorApp.Services;
using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.UnitTests.Services
{
    public class WordClonerTests
    {
        [Fact]
        public void Word_Clone_Test1()
        {
            //Arrange
            var word = new WordView("to run", "a1", "verb", "the action of moving fast by using legs");

            //Act
            var clonedWord = WordCloner.CloneWord(word);

            //Assert
            Assert.NotEqual(word, clonedWord);
            Assert.Equal(word.WordContent, clonedWord.WordContent);
            Assert.Equal(word.Lexeme, clonedWord.Lexeme);
            Assert.Equal(word.LevelAttribute, clonedWord.LevelAttribute);
            Assert.Equal(word.Defenition, clonedWord.Defenition);
        }
    }
}
