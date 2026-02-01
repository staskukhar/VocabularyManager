namespace VocabularyManager.BlazorApp.Models.Views
{
    public class WordView
    {
        public int Id { get; init; }
        public string WordContent { get; set; } = string.Empty;
        public int VocabularyId { get; set; }
        public List<MeaningView> Meanings { get; set; } = new List<MeaningView>();

        public WordView() { }

        public WordView(string wordContent)
        {
            WordContent = wordContent;
        }

        public WordView(string wordContent, List<MeaningView> meanings)
        {
            WordContent = wordContent;
            Meanings = meanings;
        }
    }
}
