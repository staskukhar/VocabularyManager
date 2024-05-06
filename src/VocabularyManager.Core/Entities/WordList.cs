using System.ComponentModel.DataAnnotations;

namespace VocabularyManager.Core.Entities
{
    public class WordList
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string ListName { get; set; }
        public string? SourceUrl { get; set; }
        public List<Word> Words { get; set; } = new List<Word> { };

        public WordList(string listName, string? sourceUrl)
        {
            ListName = listName;
            SourceUrl = sourceUrl;
        }
    }
}
