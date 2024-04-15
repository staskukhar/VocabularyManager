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
        public async Task<IEnumerable<string?>> GetImageLinksByWordAsync(string url, string word, int number, bool colorfulFilter = true)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Passed url can't be neither empty nor null.");
            }
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("Passed word param can't be neither empty nor null.");
            }
            if (number > 10)
            {
                throw new ArgumentException("Only number to 11 allowed!");
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
                .QuerySelectorAll("li.icon--item");

            if (liElements == null)
            {
                throw new TheSourceIsNotAppropriateException("The source isn't appropriate for target purpose.");
            }

            return liElements
                .Take(number)
                .Select(li => GetImageLinkFromLi(li));
        }
        public async Task<string> GetImageLinkByWordAsync(string url, string word, bool colorfulFilter = true)
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

            IElement imageLiElement = document
                .QuerySelector("section#viewport")?
                .QuerySelector("div.container.filters-inside-view")?
                .QuerySelector("div.list-content")?
                .QuerySelector("ul.icons")?
                .QuerySelector("li.icon--item");

            if (imageLiElement == null)
            {
                throw new TheSourceIsNotAppropriateException("The source isn't appropriate for target purpose.");
            }

            return GetImageLinkFromLi(imageLiElement);
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
