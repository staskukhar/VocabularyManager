using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Validators
{
    public class WordDTOValidatorTests
    {
        [Fact]
        public void Validate_Word_Test1()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordDTOValidator();
            var word = new WordDTO("to run", "a1", "verb", "the action of moving fast by using legs");

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_Test2()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordDTOValidator();
            var word = new WordDTO("to run", "a1", "verb", null);

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_Test3()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordDTOValidator();
            var word = new WordDTO("to run", "a1", null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_Test4()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordDTOValidator();
            var word = new WordDTO("to run", null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_Test5()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordDTOValidator();
            var word = new WordDTO(String.Empty, null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_Test6()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordDTOValidator();
            var word = new WordDTO(null, null, null, null);

            //Act
            var result = validator.Validate(word);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
    }
}
