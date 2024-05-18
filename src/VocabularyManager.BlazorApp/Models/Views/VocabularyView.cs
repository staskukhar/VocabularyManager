using System.ComponentModel.DataAnnotations;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.BlazorApp.Models.Views
{
    public class VocabularyView
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string? SourceUrl { get; set; }
        public List<WordView> Words { get; set; } = new List<WordView> { };

        public VocabularyView(){ }
        public VocabularyView(string name, string? sourceUrl)
        {
            Name = name;
            SourceUrl = sourceUrl;
        }
    }
}
