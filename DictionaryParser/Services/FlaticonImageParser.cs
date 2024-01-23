using AngleSharp.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using DictionaryParser.Exceptions;

namespace DictionaryParserLib.Services
{
    public class FlaticonImageParser : IImageParser<IEnumerable<string?>>
    {
        public async Task<IEnumerable<string?>> GetImageLinksByWordAsync(string url, string word, bool colorfulFilter = true)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Passed url can't be neither empty nor null.");
            }
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("Passed word param can't be neither empty nor null.");
            }

            string completeUrl = ComposeParsingURLWithWord(url, word, colorfulFilter);

            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(completeUrl);

            if (document == null)
            {
                throw new NullReferenceException("The content of source shouldn't be a null value.");
            }

            IHtmlCollection<IElement> liElements = document
                .QuerySelector("section#viewport") ?
                .QuerySelector("div.container.filters-inside-view") ?
                .QuerySelector("div.list-content") ?
                .QuerySelector("ul.icons") ?
                .QuerySelectorAll("li");

            if (liElements == null)
            {
                throw new TheSourceIsNotAppropriateException("The source isn't appropriate for target purpose.");
            }

            return liElements.Select(li => GetImageLinkFromLi(li));
        }

        private string? GetImageLinkFromLi(IElement li)
        {
            return li.GetAttribute("data-png");
        }
        private string ComposeParsingURLWithWord(string url, string word, bool colorfulFilter)
        {
            return String.Concat(
                String.Format(url, word),
                colorfulFilter ? "&color=color" : String.Empty);
        }
    }
}
