using AngleSharp;
using AngleSharp.Dom;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.UseCases.Services.Parsers;

public class SimpleOxfordDictionaryParser
    : IWordParser<string>
{
    public async IAsyncEnumerable<string> GetWordListByLinkAsync(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            throw new ArgumentNullException("Passed url can't be neither empty nor null.");
        }

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        IBrowsingContext context = BrowsingContext.New(config);
        IDocument document = await context.OpenAsync(link);

        if (document == null)
        {
            throw new NullReferenceException("The content of source shouldn't be a null value.");
        }

        IHtmlCollection<IElement>? wordsAsLiElements = document
            .QuerySelector("div#ox-container")?
            .QuerySelector("div.responsive_container.xonecolumn")?
            .QuerySelector("div.responsive_row")?
            .QuerySelector("div.responsive_entry_center")?
            .QuerySelector("div.responsive_row.responsive_entry_center_wrap")?
            .QuerySelector("div#main_column")?
            .QuerySelector("div#informational-content")?
            .QuerySelector("div#wordlistsContentPanel")?
            .QuerySelector("ul.top-g")?
            .QuerySelectorAll("li");

        if (wordsAsLiElements == null)
        {
            throw new TheSourceIsNotAppropriateException("The source isn't appropriate for target purpose.");
        }

        foreach (IElement word in wordsAsLiElements)
        {
            string? wordContent = word.QuerySelector("a")?.TextContent; // tag a contain word text

            if (!string.IsNullOrEmpty(wordContent))
            {
                yield return wordContent;
            }
        }
    }
}
