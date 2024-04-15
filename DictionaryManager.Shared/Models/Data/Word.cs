using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DictionaryManager.Shared.Models.Data
{
    public class Word
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string WordContent { get; set; }
        public string? Lexeme { get; set; }
        public string? LevelAttribute { get; set; }
        public string? Defenition { get; set; }
        public int WordListId { get; set; }
        [JsonIgnore]
        public WordList? WordList { get; set; }
        public Word(string wordContent, string? lexeme, string? levelAttribute, string? defenition)
        {
            WordContent = wordContent;
            Lexeme = lexeme;
            LevelAttribute = levelAttribute;
            Defenition = defenition;
        }
    }
}
