namespace VocabularyManager.UseCases.DTOs
{
    /// <summary>
    /// Keyless DTO for dashboard: word with its definition count, ordered by count then name.
    /// </summary>
    public class TopWordByDefinitionsDto
    {
        public int WordId { get; init; }
        public string WordContent { get; init; } = string.Empty;
        public string VocabularyName { get; init; } = string.Empty;
        public int DefinitionCount { get; init; }
    }
}
