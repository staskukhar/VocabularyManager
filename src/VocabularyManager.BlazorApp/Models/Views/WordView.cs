using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.BlazorApp.Models.Views
{
    public class WordView
    {
        public int Id { get; init; }
        public string WordContent { get; set; }
        public string? Lexeme { get; set; }
        public string? LevelAttribute { get; set; }
        public string? Defenition { get; set; }
        public int VocabularyId { get; set; }
        public WordView(){ }
        public WordView(string wordContent, string? lexeme, string? levelAttribute, string? defenition)
        {
            WordContent = wordContent;
            Lexeme = lexeme;
            LevelAttribute = levelAttribute;
            Defenition = defenition;
        }
    }
}
