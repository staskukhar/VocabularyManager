using Microsoft.AspNetCore.Mvc;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        private readonly IVocabularyStorageManager _vocabularyStoreManager;
        private readonly IWordParser<WordDTO> _oxfordDictionaryParser;
        public VocabulariesController(
            IVocabularyStorageManager vocabularyStoreManager,
            IWordParser<WordDTO> oxfordDictionaryParser)
        {
            _vocabularyStoreManager = vocabularyStoreManager;
            _oxfordDictionaryParser = oxfordDictionaryParser;
        }

        [HttpGet("byUrl")]
        public IActionResult GetVocabularyByUrlAsync([FromQuery] string url)
        {
            IAsyncEnumerable<WordDTO> vocabulary = _oxfordDictionaryParser
                    .GetWordListByLinkAsync(url, new OxfordParsingWordDTOValidator());
            return Ok(vocabulary);
        }
        [HttpPost]
        [ServiceFilter(typeof(VocabularyValidationFilter))]
        public async Task<IActionResult> CreateVocabulary(
            [FromBody] Vocabulary vocabulary)
        {
            int vocabularyId = await _vocabularyStoreManager.CreateVocabulary(vocabulary);
            return Ok(vocabularyId);
        }
        [HttpGet]
        public async Task<IActionResult> GetVocabularies()
        {
            return Ok(await _vocabularyStoreManager.GetVocabularies());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVocabularyWithWordsById(int id)
        {
            Vocabulary vocabulary = await _vocabularyStoreManager.GetVocabularyWithWordsById(id);
            return Ok(vocabulary);
        }
        [HttpPut]
        [ServiceFilter(typeof(VocabularyValidationFilter))]
        public async Task<IActionResult> UpdateVocabularyWithRelations(
            [FromBody] Vocabulary vocabulary)
        {
            await _vocabularyStoreManager.UpdateVocabulary(vocabulary);
            return Ok(vocabulary);
        }
    }
}
