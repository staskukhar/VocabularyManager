using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.Api.ActionFilters;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        private readonly IVocabularyStorageManager _vocabularyStoreManager;
        public VocabulariesController(
            IVocabularyStorageManager vocabularyStoreManager)
        {
            _vocabularyStoreManager = vocabularyStoreManager;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(VocabularyValidationFilter))]
        public async Task<IActionResult> CreateVocabulary(
            [FromBody] Vocabulary vocabulary)
        {
            int vocabularyId = await _vocabularyStoreManager.CreateVocabulary(vocabulary);
            return Ok(vocabularyId);
        }

        [HttpGet]
        public async Task<IActionResult> GetVocabularies([FromQuery] bool withWords = false)
        {
            return Ok(await _vocabularyStoreManager.GetVocabularies(withWords));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVocabularyWithWordsById(int id)
        {
            Vocabulary vocabulary = await _vocabularyStoreManager.GetVocabularyWithWordsById(id);
            return Ok(vocabulary);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(VocabularyValidationFilter))]
        public async Task<IActionResult> UpdateVocabularyWithRelations(
            [FromBody] Vocabulary vocabulary)
        {
            await _vocabularyStoreManager.UpdateVocabulary(vocabulary);
            return Ok(vocabulary);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteVocabularyById(int id)
        {
            return Ok(await _vocabularyStoreManager.Delete(id));
        }
    }
}
