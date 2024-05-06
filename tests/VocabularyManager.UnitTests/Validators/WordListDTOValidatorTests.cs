using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.UnitTests.Validators
{
    public class WordListDTOValidatorTests
    {
        [Fact]
        public void Validate_Word_List_Test1()
        {
            //Arrange
            var expectedResult = true;
            int errorsNumber = 0;
            var validator = new WordListDTOValidator();
            var wordList = new WordListDTO("somw word list", null);
            
            //Act
            var result = validator.Validate(wordList);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_List_Test2()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordListDTOValidator();
            var wordList = new WordListDTO(String.Empty, null);

            //Act
            var result = validator.Validate(wordList);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
        [Fact]
        public void Validate_Word_List_Test3()
        {
            //Arrange
            var expectedResult = false;
            int errorsNumber = 1;
            var validator = new WordListDTOValidator();
            var wordList = new WordListDTO(null, null);

            //Act
            var result = validator.Validate(wordList);

            //Assert
            Assert.Equal(expectedResult, result.IsValid);
            Assert.Equal(errorsNumber, result.Errors.Count);
        }
    }
}
