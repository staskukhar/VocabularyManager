using Ardalis.Specification;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Core.Services;

namespace VocabularyManager.UnitTests.Services
{
    public class WordStoreMangerTests
    {
        [Fact]
        public async Task Delete_Word_By_Id_Test1()
        {
            //Arramge
            IRepositoryBase<Word> repository = Substitute.For<IRepositoryBase<Word>>();
            int wordId = 0;
            var word = new Word("word", "noun", "a1", null) { Id = wordId };
            repository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(
                    Task.Run(
                        () => word
                    )
                );
            repository
                .DeleteAsync(Arg.Any<Word>())
                .Returns(
                    Task.Run(
                        () => word
                    )
                );
            IWordStoreManager wordStoreService = new WordStoreManager(repository);

            //Act
            int? deletedWordId = await wordStoreService.DeleteWordById(wordId);

            //Assert
            Assert.NotNull(deletedWordId);
            Assert.IsType<int>(wordId);
            Assert.Equal(wordId, deletedWordId);
        }
        [Fact]
        public async Task Update_Word_Test1()
        {
            //Arrange
            IRepositoryBase<Word> repository = Substitute.For<IRepositoryBase<Word>>();
            int wordId = 0;
            var word = new Word("word", "noun", "a1", null) { Id = wordId };
            repository
                .UpdateAsync(Arg.Any<Word>())
                .Returns(Task.Run(() => word));
            WordStoreManager wordStoreService = new WordStoreManager(repository);

            //Act && Assert
            await wordStoreService.UpdateWord(word);
        }
    }
}
