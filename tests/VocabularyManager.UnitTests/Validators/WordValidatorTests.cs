using FluentAssertions;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Validators
{
    public class WordValidatorTests
    {
        [Fact]
        public void Validate_Word_Test1()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordValidator();
            var word = new Word("to run", "a1", "verb", "the action of moving fast by using legs");

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber); ;
        }
        [Fact]
        public void Validate_Word_Test2()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordValidator();
            var word = new Word("to run", "a1", "verb", null);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Word_Test3()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordValidator();
            var word = new Word("to run", "a1", null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Word_Test4()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordValidator();
            var word = new Word("to run", null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Word_Test5()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordValidator();
            var word = new Word(String.Empty, null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Word_Test6()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordValidator();
            var word = new Word(null, null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
    }
}
