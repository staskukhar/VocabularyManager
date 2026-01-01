using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.BlazorApp.Services
{
    public class WordCloner
    {
        public static WordView CloneWord(WordView word)
        {
            WordView clone = new WordView()
            {
                Id = word.Id,
                WordContent = word.WordContent,
                Lexeme = word.Lexeme,
                LevelAttribute = word.LevelAttribute,
                Defenition = word.Defenition,
                VocabularyId = word.VocabularyId,
            };
            return clone;
        }
    }
}
