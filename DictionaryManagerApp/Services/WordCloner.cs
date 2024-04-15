using DictionaryManager.Shared.Models.DTOs;

namespace DictionaryManagerApp.Services
{
    public class WordCloner
    {
        public static WordDTO CloneWord(WordDTO word)
        {
            WordDTO clone = new WordDTO()
            {
                Id = word.Id,
                WordContent = word.WordContent,
                Lexeme = word.Lexeme,
                LevelAttribute = word.LevelAttribute,
                Defenition = word.Defenition,
                WordListId = word.WordListId,
            };
            return clone;
        }
    }
}
