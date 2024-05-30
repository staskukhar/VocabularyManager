using Ardalis.Specification;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.StoreManagers;

namespace VocabularyManager.UnitTests.Services
{
    public class WordStoreMangerTests
    {
        Fixture _fixture;
        public WordStoreMangerTests()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public async Task Delete_Word_By_Id_Test1()
        {
            //Arramge
            IRepositoryBase<Word> repository = Substitute.For<IRepositoryBase<Word>>();
            int wordId = _fixture.Create<int>();
            var word = new Word(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(), 
                null
            ) { Id = wordId };
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
            IWordStorageManager wordStoreService = new WordStorageManager(repository);

            //Act
            int? deletedWordId = await wordStoreService.DeleteWordById(wordId);

            //Assert
            deletedWordId
                .Should().NotBeNull()
                .And.Be(wordId);
            wordId.Should().BeOfType(typeof(int));
        }
        [Fact]
        public async Task Update_Word_Test1()
        {
            //Arrange
            IRepositoryBase<Word> repository = Substitute.For<IRepositoryBase<Word>>();
            int wordId = _fixture.Create<int>();
            var word = new Word(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(), 
                null
            ) { Id = wordId };
            repository
                .UpdateAsync(Arg.Any<Word>())
                .Returns(Task.Run(() => word));
            WordStorageManager wordStoreService = new WordStorageManager(repository);

            //Act && Assert
            await wordStoreService.UpdateWord(word);
        }
    }
}
