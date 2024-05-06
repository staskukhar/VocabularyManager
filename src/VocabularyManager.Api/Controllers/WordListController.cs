using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.Infrastructure.Validators;
using VocabularyManager.Infrastructure.Exceptions;
using VocabularyManager.UseCases.DTOs;
using FluentValidation;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListController : ControllerBase
    {
        private readonly IWordListService _wordListService;
        private readonly IWordParser<Word> _oxfordDictionaryParser;
        private readonly AbstractValidator<WordListDTO> _wordListValidator;
        public WordListController(
            IWordListService wordListService,
            IWordParser<Word> oxfordDictionaryParser,
            AbstractValidator<WordListDTO> wordListValidator)
        {
            _wordListService = wordListService;
            _oxfordDictionaryParser = oxfordDictionaryParser;
            _wordListValidator = wordListValidator;
        }

        [HttpPost("getwordlist")]
        public async Task<IActionResult> GetWordListByUrlAsync([FromBody] string url)
        {
            try
            {
                IEnumerable<Word> wordList = await _oxfordDictionaryParser
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
        public async Task<IActionResult> CreateWordList(
            [FromBody] WordListDTO wordList)
        {
            try
            {
                var validationResult = _wordListValidator.Validate(wordList);
                if(!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                int wordListId = await _wordListService.CreateWordList(
                    new WordList(
                        listName: wordList.ListName,
                        sourceUrl: wordList.SourceUrl
                    )
                    {
                        Words = wordList.Words.Select(w => new Word
                        (
                            wordContent: w.WordContent,
                            lexeme: w.Lexeme,
                            levelAttribute: w.LevelAttribute,
                            defenition: w.Defenition
                        )).ToList()
                    });
                return Ok(wordListId);
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the create operation failed!");
            }
        }
        [HttpGet("getwordlists")]
        public async Task<IActionResult> GetWordLists()
        {
            return Ok(await _wordListService.GetWordLists());
        }
        [HttpGet("getwordlistbyid")]
        public async Task<IActionResult> GetWordListWithWordsById(
            [FromQuery] int wordListId
        )
        {
            try
            {
                WordList wordList = await _wordListService.GetWordListByIdWithWords(wordListId);

                return Ok(wordList);
            }
            catch(ArgumentNullException) 
            {
                return BadRequest("Id parameter is required and should be defined.");
            }
            catch(InvalidOperationException)
            {
                return BadRequest($"There isn't any list with id: {wordListId}");
            }
        }
        [HttpPut("updatewordlist")]
        public async Task<IActionResult> UpdateDictionaryWithRelations(
            [FromBody] WordListDTO wordList)
        {
            try
            {
                var validationResult = _wordListValidator.Validate(wordList);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                await _wordListService.UpdateWordList(
                    new WordList(
                        listName: wordList.ListName,
                        sourceUrl: wordList.SourceUrl
                    )
                    {
                        Words = wordList.Words.Select(w => new Word
                        (
                            wordContent: w.WordContent,
                            lexeme: w.Lexeme,
                            levelAttribute: w.LevelAttribute,
                            defenition: w.Defenition
                        )).ToList()
                    });
                return Ok(wordList);
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the update operation failed!");
            }
        }
    }
}
