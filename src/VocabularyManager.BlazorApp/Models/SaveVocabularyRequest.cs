namespace VocabularyManager.BlazorApp.Models
{
    public class SaveVocabularyRequest
    {
        public bool SaveToNew { get; set; }
        public string VocabularyName { get; set; } = string.Empty;
        public int SelectedVocabularyId { get; set; }
    }
}
