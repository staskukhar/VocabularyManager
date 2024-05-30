using FluentAssertions;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Validators
{
    public class VocabularyValidatorTests
    {
        [Fact]
        public void Validate_Vocabulary_Test1()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new VocabularyValidator();
            var vocabulary = new Vocabulary("some word list", null);
            
            //Act
            var result = validator.Validate(vocabulary);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Vocabulary_Test2()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new VocabularyValidator();
            var vocabulary = new Vocabulary(String.Empty, null);

            //Act
            var result = validator.Validate(vocabulary);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
        [Fact]
        public void Validate_Vocabulary_Test3()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new VocabularyValidator();
            var vocabulary = new Vocabulary(null, null);

            //Act
            var result = validator.Validate(vocabulary);

            //Assert
            result.IsValid
                .Should().Be(expectedResult);
            result.Errors.Count
                .Should().Be(errorsNumber);
        }
    }
}
