using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VocabularyManager.Core.Entities
{
    public class Meaning
    {
        [Required]
        public int Id { get; init; }

        public string? LexemeType { get; set; }

        public string? Definition { get; set; }

        public string? Level { get; set; }

        public int WordId { get; set; }

        [JsonIgnore]
        public Word? Word { get; set; }

        public Meaning() { }

        public Meaning(string? lexemeType, string? definition, string? level)
        {
            LexemeType = lexemeType;
            Definition = definition;
            Level = level;
        }
    }
}
