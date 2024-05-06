using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Services
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
