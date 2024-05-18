using Microsoft.AspNetCore.Mvc;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordStoreManager _wordStoreManager;
        private readonly IVocabularyStoreManager _vocabularyStoreManager;
        public WordsController(
            IWordStoreManager wordStoreManager,
            IVocabularyStoreManager vocabularyStoreManager)
        {
            _wordStoreManager = wordStoreManager;  
            _vocabularyStoreManager = vocabularyStoreManager;
        }
        [HttpPost]
        [ServiceFilter(typeof(WordValidationFilter))]
        public async Task<IActionResult> AddWord(
            [FromQuery] int vocabularyId,
            [FromBody] Word word)
        {
            int addedWordId = await _vocabularyStoreManager.AddWord(word, vocabularyId);
            return Ok(addedWordId);
        }
        [HttpPost("addrange")]
        [ServiceFilter(typeof(WordsValidationFilter))]
        public async Task<IActionResult> AddWords(
            [FromQuery] int vocabularyId,
            [FromBody] IEnumerable<Word> words)
        {
            await _vocabularyStoreManager.AddWords(words, vocabularyId);
            return Ok();
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
