using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Exceptions;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Validators;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IWordListService _wordListService;
        private readonly AbstractValidator<WordDTO> _wordValidator;
        public WordController(
            IWordService wordService,
            IWordListService wordListService,
            AbstractValidator<WordDTO> wordValidator)
        {
            _wordService = wordService;  
            _wordListService = wordListService;
            _wordValidator = wordValidator;
        }
        [HttpPost]
        [Route("addword")]
        public async Task<IActionResult> AddWordToWordList(
            [FromQuery] int wordListId,
            [FromBody] WordDTO word)
        {

            try
            {
                var validationResult = _wordValidator.Validate(word);
                if(!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                int addedWordId = await _wordListService.AddWordToWordList(
                    new Word(
                        wordContent: word.WordContent, 
                        lexeme: word.Lexeme,
                        levelAttribute: word.LevelAttribute,
                        defenition: word.Defenition
                    ),
                    wordListId);
                return Ok(addedWordId);
            }
            catch(WordListNotFoundException)
            {
                return BadRequest($"Sorry, there isn't any word list with id: {wordListId}");
            }
        }
        [HttpPost]
        [Route("addwords")]
        public async Task<IActionResult> AddWordsToWordList(
            [FromQuery] int wordListId,
            [FromBody] IEnumerable<WordDTO> words)
        {
            try
            {
                IList<Word> mappedWords = new List<Word>();
                foreach(WordDTO word in words)
                {
                    var validationResult = _wordValidator.Validate(word);
                    if(!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }
                    mappedWords.Add(
                        new Word(
                            wordContent: word.WordContent,
                            lexeme: word.Lexeme,
                            levelAttribute: word.LevelAttribute,
                            defenition: word.Defenition
                        )
                    );
                }
                await _wordListService.AddWordsToWordList(
                    mappedWords, 
                    wordListId);
                return Ok();
            }
            catch(WordListNotFoundException)
            {
                return BadRequest($"Sorry, there isn't any word list with id: {wordListId}");
            }
        }
        [HttpDelete("deleteword")]
        public async Task<IActionResult> DeleteWord([FromQuery] int id)
        {
            try
            {
                await _wordService.DeleteWordById(id);
                return Ok($"Word by id: {id} removed successfuly.");
            }
            catch(WordNotFoundException)
            {
                return BadRequest($"Sorry, there is not any word with id: {id}");
            }
        }
        [HttpPut("updateword")]
        public async Task<IActionResult> UpdateWord([FromBody] WordDTO word)
        {
            var validationResult = _wordValidator.Validate(word);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _wordService.UpdateWord(
                new Word(
                    wordContent: word.WordContent,
                    lexeme: word.Lexeme,
                    levelAttribute: word.LevelAttribute,
                    defenition: word.Defenition
                ));
            return Ok();
        }
    }
}
