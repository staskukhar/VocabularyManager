using Ardalis.Specification;
using FluentAssertions;
using NSubstitute;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Services;

namespace VocabularyManager.UnitTests.Services
{
    public class AnkiExportServiceTests
    {
        private readonly IRepositoryBase<Vocabulary> _vocabularyRepository;
        private readonly AnkiExportService _service;

        public AnkiExportServiceTests()
        {
            _vocabularyRepository = Substitute.For<IRepositoryBase<Vocabulary>>();
            _service = new AnkiExportService(_vocabularyRepository);
        }

        [Fact]
        public async Task ExportAsync_WhenVocabularyNotFound_ReturnsNull()
        {
            _vocabularyRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Vocabulary>>(), Arg.Any<CancellationToken>())
                .Returns((Vocabulary?)null);

            var result = await _service.ExportAsync(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task ExportAsync_StartsWithRequiredAnkiHeaders()
        {
            var vocabulary = VocabularyWithWords("My Deck", new Word("hello")
            {
                Meanings = [new Meaning("noun", "a greeting", null)]
            });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().StartWith(
                "#separator:tab\n#html:true\n#notetype:Basic (and reversed card)\n#deck:My Deck\n");
        }

        [Fact]
        public async Task ExportAsync_SkipsWordsWithNoMeanings()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("hello") { Meanings = [new Meaning("noun", "a greeting", null)] },
                new Word("empty") { Meanings = [] });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("hello");
            text.Should().NotContain("empty");
        }

        [Fact]
        public async Task ExportAsync_SkipsMeaningsWithNullOrEmptyDefinition()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("hello")
                {
                    Meanings =
                    [
                        new Meaning("noun", null, null),
                        new Meaning("verb", "", null),
                        new Meaning("expr", "valid definition", null)
                    ]
                });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("valid definition");
            text.Should().NotContain("<br>valid definition");
        }

        [Fact]
        public async Task ExportAsync_RendersDefinitionWithLexemeType()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("run") { Meanings = [new Meaning("verb", "to move fast", null)] });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("to move fast [verb]");
        }

        [Fact]
        public async Task ExportAsync_RendersDefinitionWithoutBracketsWhenNoLexemeType()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("run") { Meanings = [new Meaning(null, "to move fast", null)] });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("to move fast");
            text.Should().NotContain("[");
        }

        [Fact]
        public async Task ExportAsync_JoinsMeaningsWithHtmlLineBreak()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("set")
                {
                    Meanings =
                    [
                        new Meaning("noun", "a collection", null),
                        new Meaning("verb", "to place", null)
                    ]
                });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("a collection [noun]<br>to place [verb]");
        }

        [Fact]
        public async Task ExportAsync_EscapesHtmlSpecialCharsInWordContent()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("<b>bold</b> & more") { Meanings = [new Meaning(null, "definition", null)] });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("&lt;b&gt;bold&lt;/b&gt; &amp; more");
        }

        [Fact]
        public async Task ExportAsync_EscapesHtmlSpecialCharsInDefinitionAndLexemeType()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("word")
                {
                    Meanings = [new Meaning("<em>noun</em>", "def & <more>", null)]
                });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("def &amp; &lt;more&gt; [&lt;em&gt;noun&lt;/em&gt;]");
        }

        [Fact]
        public async Task ExportAsync_UsesTabAsSeparatorBetweenFrontAndBack()
        {
            var vocabulary = VocabularyWithWords("Deck",
                new Word("apple") { Meanings = [new Meaning(null, "a fruit", null)] });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            var text = GetText(result!);
            text.Should().Contain("apple\ta fruit");
        }

        [Fact]
        public async Task ExportAsync_FileNameUsesVocabularyName()
        {
            var vocabulary = VocabularyWithWords("English B2", new Word("go")
            {
                Meanings = [new Meaning(null, "to move", null)]
            });
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(1);

            result!.FileName.Should().Be("English B2.txt");
        }

        [Fact]
        public async Task ExportAsync_FileNameFallsBackToIdWhenNameHasOnlyInvalidChars()
        {
            var vocabulary = new Vocabulary("///", null)
            {
                Id = 42,
                Words = [new Word("go") { Meanings = [new Meaning(null, "to move", null)] }]
            };
            SetupRepository(vocabulary);

            var result = await _service.ExportAsync(42);

            result!.FileName.Should().Be("vocabulary-42.txt");
        }

        private void SetupRepository(Vocabulary vocabulary)
        {
            _vocabularyRepository
                .FirstOrDefaultAsync(Arg.Any<ISpecification<Vocabulary>>(), Arg.Any<CancellationToken>())
                .Returns(vocabulary);
        }

        private static Vocabulary VocabularyWithWords(string name, params Word[] words)
        {
            var v = new Vocabulary(name, null) { Words = words.ToList() };
            foreach (var w in words) w.VocabularyId = v.Id;
            return v;
        }

        private static string GetText(AnkiExportResult result) =>
            System.Text.Encoding.UTF8.GetString(result.Content);
    }
}
