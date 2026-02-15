using System.ComponentModel.DataAnnotations;

namespace VocabularyManager.Core.Entities
{
    public class Vocabulary
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string Name { get; set; }
        public string? SourceUrl { get; set; }
        public List<Word> Words { get; set; } = [];
        public Vocabulary(string name, string? sourceUrl)
        {
            Name = name;
            SourceUrl = sourceUrl;
        }
    }
}
