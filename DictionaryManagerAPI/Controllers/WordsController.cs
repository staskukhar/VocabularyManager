using DictionaryManager.Shared.Models.Data;
using DictionaryManager.Shared.Models.DTOs;
using DictionaryManagerAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;
        private readonly IWordListRepository _wordListRepository;
        public WordsController(
            IWordRepository wordRepository, 
            IWordListRepository wordListRepository)
        {
            _wordRepository = wordRepository;
            _wordListRepository = wordListRepository;   
        }
        [HttpPost]
        [Route("addword")]
        public IActionResult AddWordToWordList(
            [FromQuery] int wordListId,
            [FromBody] WordDTO word)
        {
            try
            {
                WordList wordList = _wordListRepository
                    .GetAll()
                    .Single(w => w.Id == wordListId);
                Word wordToCreate = new Word(
                        word.WordContent,
                        word.Lexeme,
                        word.LevelAttribute,
                        word.Defenition
                    )
                {
                    WordListId = wordListId
                };
                Word createdWord = _wordRepository.Add(
                    wordToCreate
                );
                _wordListRepository.Save();
                return Ok(createdWord);
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest($"Sorry, the list with id: {wordListId} does not exist!");
            }
            catch (Exception)
            {
                return BadRequest("Sorry, the operation failed!");
            }
        }
        [HttpPost]
        [Route("addwords")]
        public IActionResult AddWordsToWordList(
            [FromQuery] int wordListId,
            [FromBody] List<WordDTO> words)
        {
            try
            {
                WordList wordList = _wordListRepository
                    .GetAll()
                    .Single(w => w.Id == wordListId);

                wordList.Words.AddRange(
                    words.Select(w =>
                        new Word(
                            w.WordContent,
                            w.Lexeme,
                            w.LevelAttribute,
                            w.Defenition
                        )
                        { WordListId = wordListId}
                    )
                );
            }
            catch (Exception) 
            {
                return BadRequest($"Sorry, the list with id: {wordListId} does not exist!");
            }
            try
            {
                _wordListRepository.Save();
                return Ok($"The words added to list with id: {wordListId}.");
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the operation failed!");
            }
        }
        [HttpDelete("deleteword")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                _wordRepository.Delete(_wordRepository.GetById(id));
                _wordListRepository.Save();
                return Ok($"Word by id: {id} removed successfuly.");
            }
            catch(Exception ex) when (
                ex is ArgumentNullException || ex is InvalidOperationException)
            {
                return BadRequest($"Sorry, there is not any  word by id: {id}");
            }
            catch (Exception ex) when (
                ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                return BadRequest("Sorry, the operation failed!");
            }
        }
    }
}
