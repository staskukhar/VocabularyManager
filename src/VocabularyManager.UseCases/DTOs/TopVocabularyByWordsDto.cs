namespace VocabularyManager.UseCases.DTOs
{
    /// <summary>
    /// Keyless DTO for dashboard: vocabulary with its word count, ordered by count.
    /// </summary>
    public class TopVocabularyByWordsDto
    {
        public int VocabularyId { get; init; }
        public string VocabularyName { get; init; } = string.Empty;
        public int WordCount { get; init; }
    }
}
