using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VocabularyManager.Core.Entities
{
    public class Word
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public string WordContent { get; set; } = string.Empty;

        public int VocabularyId { get; set; }

        [JsonIgnore]
        public Vocabulary? Vocabulary { get; set; }

        public List<Meaning> Meanings { get; set; } = new List<Meaning>();

        public Word() { }

        public Word(string wordContent)
        {
            WordContent = wordContent;
        }
    }
}
