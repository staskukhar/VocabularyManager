namespace VocabularyManager.Core.Entities
{
    public class Word
    {
        public int Id { get; init; }

        public string WordContent { get; set; } = string.Empty;

        public int VocabularyId { get; set; }

        public Vocabulary? Vocabulary { get; set; }

        public List<Meaning> Meanings { get; set; } = new List<Meaning>();

        public Word() { }

        public Word(string wordContent)
        {
            WordContent = wordContent;
        }
    }
}
