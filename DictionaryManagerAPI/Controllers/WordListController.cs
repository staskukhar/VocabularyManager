using DictionaryManager.Shared.Models.Data;
using DictionaryManager.Shared.Models.DTOs;
using DictionaryManagerAPI.Services.Repositories;
using DictionaryParser.Exceptions;
using DictionaryParser.Services;
using DictionaryParserLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListController : ControllerBase
    {
        private readonly IWordListRepository _wordListRepository;
        public WordListController(IWordListRepository wordListRepository)
        {
            _wordListRepository = wordListRepository;
        }

        [HttpPost("getwordlist")]
        public async Task<IActionResult> GetWordListByUrlAsync([FromBody] string url)
        {
            var dictionaryParser = new OxfordDictionaryParser();
            try
            {
                IEnumerable<WordDTO> wordList = await dictionaryParser
                    .GetWordListByLinkAsync(url, OxfordDictionaryValidator.IsWordObjectDataValid);

                return Ok(wordList);
            }
            catch(Exception ex) when(
                ex is ArgumentNullException || 
                ex is NullReferenceException || 
                ex is TheSourceIsNotAppropriateException
            )
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("createwordlist")]
        public IActionResult CreateWordList(
            [FromBody] WordListDTO wordList)
        {
            WordList wlResult = _wordListRepository.Add(
                new WordList(wordList.ListName, wordList.SourceUrl));
            try
            {
                _wordListRepository.Save();
                return Ok(wlResult.Id);
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the create operation failed!");
            }
        }
        [HttpGet("getdictionaries")]
        public IActionResult GetDictionaries()
        {
            return Ok(_wordListRepository.GetAll());
        }
        [HttpGet("getwordlistbyid")]
        public IActionResult GetWordListWithWordsByDictionaryId(
            [FromQuery] int dictionaryId
        )
        {
            try
            {
                WordList wordList = _wordListRepository.GetWithRelationsById(dictionaryId);

                return Ok(wordList);
            }
            catch(ArgumentNullException) 
            {
                return BadRequest("Id parameter is required and should be defined.");
            }
            catch(InvalidOperationException)
            {
                return BadRequest($"There isn't any list with id: {dictionaryId}");
            }
        }
        [HttpPut("updatewordlist")]
        public IActionResult UpdateDictionaryWithRelations(
            [FromBody] WordList wordList)
        {
            WordList updatedWordList = _wordListRepository.Update(wordList);
            try
            {
                _wordListRepository.Save();
                return Ok(updatedWordList.Id);
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the update operation failed!");
            }
        }
    }
}
