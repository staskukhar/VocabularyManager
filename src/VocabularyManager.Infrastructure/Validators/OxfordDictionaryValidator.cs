using VocabularyManager.Core.Entities;

namespace VocabularyManager.Infrastructure.Validators
{
    public class OxfordDictionaryValidator
    {
        public static bool IsWordObjectDataValid(Word word)
        {
            if (string.IsNullOrEmpty(word.WordContent)) return false;
            if (string.IsNullOrEmpty(word.Lexeme)) return false;
            if (string.IsNullOrEmpty(word.LevelAttribute)) return false;
            return true;
        }
    }
}
