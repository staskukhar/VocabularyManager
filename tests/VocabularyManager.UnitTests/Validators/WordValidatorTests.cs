using FluentAssertions;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Validators
{
    public class WordValidatorTests
    {
        [Fact]
        public void Validate_Word_With_Valid_Content_Returns_Valid()
        {
            //Arrange
            var validator = new WordValidator();
            var word = new Word("to run");

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Validate_Word_With_Empty_Content_Returns_Invalid()
        {
            //Arrange
            var validator = new WordValidator();
            var word = new Word(string.Empty);

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Validate_Word_With_Content_Exceeding_MaxLength_Returns_Invalid()
        {
            //Arrange
            var validator = new WordValidator();
            var word = new Word(new string('a', 201)); // 201 characters, exceeds 200 limit

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Validate_Word_With_Max_Length_Content_Returns_Valid()
        {
            //Arrange
            var validator = new WordValidator();
            var word = new Word(new string('a', 200)); // Exactly 200 characters

            //Act
            var result = validator.Validate(word);

            //Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Be(0);
        }
    }
}
