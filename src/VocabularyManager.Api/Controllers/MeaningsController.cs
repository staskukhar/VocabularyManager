using Microsoft.AspNetCore.Mvc;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeaningsController : ControllerBase
    {
        private readonly IMeaningStorageManager _meaningStorageManager;

        public MeaningsController(IMeaningStorageManager meaningStorageManager)
        {
            _meaningStorageManager = meaningStorageManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(MeaningValidationFilter))]
        public async Task<IActionResult> AddMeaning(
            [FromQuery] int wordId,
            [FromBody] Meaning meaning)
        {
            int id = await _meaningStorageManager.AddMeaning(meaning, wordId);
            return Ok(id);
        }

        [HttpPut]
        [ServiceFilter(typeof(MeaningValidationFilter))]
        public async Task<IActionResult> UpdateMeaning([FromBody] Meaning meaning)
        {
            await _meaningStorageManager.UpdateMeaning(meaning);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMeaning([FromRoute] int id)
        {
            await _meaningStorageManager.DeleteMeaningById(id);
            return Ok($"Meaning with id: {id} removed successfully.");
        }
    }
}
