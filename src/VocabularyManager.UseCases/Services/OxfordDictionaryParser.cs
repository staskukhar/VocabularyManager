using AngleSharp;
using AngleSharp.Dom;
using FluentValidation;
using FluentValidation.Results;
using VocabularyManager.UseCases.Exceptions;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.UseCases.Services
{
    public class OxfordDictionaryParser : IWordParser<WordDTO>
    {
        public async IAsyncEnumerable<WordDTO> GetWordListByLinkAsync(
            string url, 
            IValidator<WordDTO> validationFilter
        )
        {
            if (String.IsNullOrEmpty(url)) 
            {
                throw new ArgumentNullException("Passed url can't be neither empty nor null."); 
            }

            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(url);

            if(document == null)
            {
                throw new NullReferenceException("The content of source shouldn't be a null value.");
            }

            IHtmlCollection<IElement> wordsAsLiElements = document 
                .QuerySelector("div#ox-container") ?
                .QuerySelector("div.responsive_container.xonecolumn") ?
                .QuerySelector("div.responsive_row") ?
                .QuerySelector("div.responsive_entry_center") ?
                .QuerySelector("div.responsive_row.responsive_entry_center_wrap") ?
                .QuerySelector("div#main_column") ?
                .QuerySelector("div#informational-content") ?
                .QuerySelector("div#wordlistsContentPanel") ?
                .QuerySelector("ul.top-g") ?
                .QuerySelectorAll("li");

            if(wordsAsLiElements == null)
            {
                throw new TheSourceIsNotAppropriateException("The source isn't appropriate for target purpose.");
            }

            IEnumerable<WordDTO> words = wordsAsLiElements.Select(li => GetDataAsObject(li));
            foreach(WordDTO word in words)
            {
                ValidationResult result = validationFilter.Validate(word);
                if (result.IsValid)
                {
                    yield return word;
                }
            }
        }
        public WordDTO GetDataAsObject(IElement element)
        {
            var nestedDiv = element.QuerySelector("div"); // should contain level attribute and audio content

            return new WordDTO(
                wordContent: element.QuerySelector("a")?.TextContent, // tag a contain word text
                lexeme: element.QuerySelector(".pos")?.TextContent, // span with class .pos contain lexema info
                levelAttribute: nestedDiv?.QuerySelector("span.belong-to")?.TextContent, //tag div contain level info
                defenition: null
            );
        }
    }
}
