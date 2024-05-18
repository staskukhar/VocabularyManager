using Microsoft.AspNetCore.Mvc;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.Api.ActionFilters;
using FluentValidation;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        private readonly IVocabularyStoreManager _vocabularyStoreManager;
        private readonly IWordParser<WordDTO> _oxfordDictionaryParser;
        public VocabulariesController(
            IVocabularyStoreManager vocabularyStoreManager,
            IWordParser<WordDTO> oxfordDictionaryParser)
        {
            _vocabularyStoreManager = vocabularyStoreManager;
            _oxfordDictionaryParser = oxfordDictionaryParser;
        }

        [HttpGet]
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
        [HttpGet("all")]
        public async Task<IActionResult> GetVocabularies()
        {
            return Ok(await _vocabularyStoreManager.GetVocabularies());
        }
        [HttpGet("byId")]
        public async Task<IActionResult> GetVocabularyWithWordsById(
            [FromQuery] int vocabularyId
        )
        {
            Vocabulary vocabulary = await _vocabularyStoreManager.GetVocabularyWithWordsById(vocabularyId);
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
