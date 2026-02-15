namespace VocabularyManager.BlazorApp.Models.Views
{
    public class VocabularyView
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string? SourceUrl { get; set; }
        public List<WordView> Words { get; set; } = [];

        public VocabularyView() { }
        public VocabularyView(string name, string? sourceUrl)
        {
            Name = name;
            SourceUrl = sourceUrl;
        }
    }
}
