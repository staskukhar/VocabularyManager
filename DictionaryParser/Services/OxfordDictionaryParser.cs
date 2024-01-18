using AngleSharp;
using AngleSharp.Dom;
using OxfordDictionaryParser.Exceptions;

namespace OxfordDictionaryParser.Services
{
    public class OxfordDictionaryParser : IWordParser<ParsingWordDTO>
    {
        public async Task<IEnumerable<ParsingWordDTO>> GetWordListByLinkAsync(string url)
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

            return wordsAsLiElements.Select(li => GetDataAsObject(li));
        }
        public ParsingWordDTO GetDataAsObject(IElement element)
        {
            var nestedDiv = element.QuerySelector("div"); // should contain level attribute and audio content
            var audioContentDivs = nestedDiv?.QuerySelectorAll("div"); // audio content with attribute "data-src-mp3" containt only partisional link

            return new ParsingWordDTO(
                originWord: element.QuerySelector("a")?.TextContent, // tag a contain word text
                lexemaType: element.QuerySelector(".pos")?.TextContent, // span with class .pos contain lexema info
                levelAttribute: nestedDiv?.QuerySelector("div")?.TextContent, //tag div contain level info
                audioLinks: audioContentDivs?.Select( audio => 
                    audio.GetAttribute("data-src-mp3") ).ToArray()
                );
        }
    }
}
