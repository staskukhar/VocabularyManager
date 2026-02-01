using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordStorageManager _wordStoreManager;
        private readonly IVocabularyStorageManager _vocabularyStoreManager;
        private readonly IWordParser<string> _oxfordDictionaryParser;
        public WordsController(
            IWordStorageManager wordStoreManager,
            IVocabularyStorageManager vocabularyStoreManager,
            IWordParser<string> oxfordDictionaryParser)
        {
            _wordStoreManager = wordStoreManager;  
            _vocabularyStoreManager = vocabularyStoreManager;
            _oxfordDictionaryParser = oxfordDictionaryParser;
        }
        [HttpGet]
        public ActionResult<PlainListOfWordsResponseDTO> GetListOfWordsByUrlAsync([FromQuery] string url)
        {
            IAsyncEnumerable<string> listOfWords = _oxfordDictionaryParser
                    .GetWordListByLinkAsync(url);
            return Ok(
                new PlainListOfWordsResponseDTO(
                    listOfWords.ToBlockingEnumerable().Distinct()));
        }
        [HttpPost]
        [ServiceFilter(typeof(WordsValidationFilter))]
        public async Task<IActionResult> AddWords(
            [FromQuery] int vocabularyId,
            [FromBody] IEnumerable<Word> words)
        {
            ImmutableList<int> ids = await _vocabularyStoreManager.AddWords(words, vocabularyId);
            return Ok(ids);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWord([FromRoute] int id)
        {
            await _wordStoreManager.DeleteWordById(id);
            return Ok($"Word by id: {id} removed successfuly.");
        }
        [HttpPut]
        [ServiceFilter(typeof(WordValidationFilter))]
        public async Task<IActionResult> UpdateWord([FromBody] Word word)
        {
            await _wordStoreManager.UpdateWord(word);
            return Ok();
        }
    }
}
