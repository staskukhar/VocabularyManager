using Microsoft.AspNetCore.Mvc;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Api.Controllers
{
    /// <summary>
    /// Parses word lists from external sources (e.g. URLs).
    /// Keeps GET /api/Words/{id} dedicated to fetching a single word by id.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WordParserController : ControllerBase
    {
        private readonly IWordParser<string> _oxfordDictionaryParser;

        public WordParserController(IWordParser<string> oxfordDictionaryParser)
        {
            _oxfordDictionaryParser = oxfordDictionaryParser;
        }

        /// <summary>
        /// GET api/WordParser?url=...
        /// Returns a distinct list of words parsed from the given URL.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PlainListOfWordsResponseDTO>> ParseListByUrl([FromQuery] string url)
        {
            List<string> words = new();
            await foreach (string word in _oxfordDictionaryParser.GetWordListByLinkAsync(url))
                words.Add(word);
            return Ok(new PlainListOfWordsResponseDTO(words.Distinct()));
        }
    }
}
