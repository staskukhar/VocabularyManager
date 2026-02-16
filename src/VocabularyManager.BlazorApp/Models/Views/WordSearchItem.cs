namespace VocabularyManager.BlazorApp.Models.Views
{
    /// <summary>
    /// Represents a word in search results with its vocabulary name for display.
    /// The same word can appear in multiple vocabularies.
    /// </summary>
    public class WordSearchItem
    {
        public required WordView Word { get; init; }
        public required string VocabularyName { get; init; }

        /// <summary>
        /// Display text for the search box: "word (Vocabulary name)".
        /// </summary>
        public string DisplayText => $"{Word.WordContent} ({VocabularyName})";
    }
}
