using DictionaryParser.Exceptions;
using DictionaryParser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListController : ControllerBase
    {
        [HttpPost("getwordlist")]
        public async Task<IActionResult> GetWordListByUrlAsync([FromBody] string url)
        {
            var dictionaryParser = new OxfordDictionaryParser();
            try
            {
                IEnumerable<ParsingWordDTO> wordList = await dictionaryParser.GetWordListByLinkAsync(url);

                return Ok(wordList);
            }
            catch (ArgumentNullException e) 
            {
                return BadRequest(e.Message);
            }
            catch(NullReferenceException e) 
            { 
                return BadRequest(e.Message);
            }
            catch(TheSourceIsNotAppropriateException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
