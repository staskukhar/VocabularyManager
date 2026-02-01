namespace VocabularyManager.BlazorApp.Models.Views
{
    public class MeaningView
    {
        public int Id { get; init; }
        public string? LexemeType { get; set; }
        public string? Definition { get; set; }
        public string? Level { get; set; }
        public int WordId { get; set; }

        public MeaningView() { }

        public MeaningView(string? lexemeType, string? definition, string? level)
        {
            LexemeType = lexemeType;
            Definition = definition;
            Level = level;
        }
    }
}
