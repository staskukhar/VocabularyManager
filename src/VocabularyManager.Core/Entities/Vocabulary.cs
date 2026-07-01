namespace VocabularyManager.Core.Entities
{
    public class Vocabulary
    {
        public int Id { get; init; }
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
