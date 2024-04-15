using DictionaryManager.Shared.Models.Data;

namespace DictionaryManager.Shared.Models.DTOs
{
    public class WordDTO
    {
        public int? Id { get; init; }
        public string WordContent { get; set; }
        public string? LevelAttribute { get; set; }
        public string? Lexeme { get; set; }
        public string? Defenition { get; set; }
        public int WordListId { get; set; }
        public WordDTO(
            string wordContent,
            string? levelAttribute,
            string? lexeme,
            string? defenition
        )
        {
            WordContent = wordContent;
            LevelAttribute = levelAttribute;
            Lexeme = lexeme;
            Defenition = defenition;
        }
        public WordDTO(Word wordToMap) 
        {
            Id = wordToMap.Id;
            WordContent = wordToMap.WordContent;
            LevelAttribute = wordToMap.LevelAttribute;
            Lexeme = wordToMap.Lexeme;
            Defenition = wordToMap.Defenition;
        }
        public WordDTO() { }
    }   
}