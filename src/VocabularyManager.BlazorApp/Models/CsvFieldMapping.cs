namespace VocabularyManager.BlazorApp.Models
{
    /// <summary>
    /// Represents a mapping between a Word/Meaning model property and a CSV column.
    /// </summary>
    public class CsvFieldMapping
    {
        public string ModelProperty { get; set; } = string.Empty;
        public string ModelPropertyDisplayName { get; set; } = string.Empty;
        public string? CsvColumnName { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMeaningProperty { get; set; }
    }

    /// <summary>
    /// Provides available Word and Meaning model properties for CSV mapping.
    /// </summary>
    public static class WordModelProperties
    {
        public const string WordContent = "WordContent";
        public const string LexemeType = "LexemeType";
        public const string Level = "Level";
        public const string Definition = "Definition";

        public static List<CsvFieldMapping> GetMappingFields() => new()
        {
            new CsvFieldMapping
            {
                ModelProperty = WordContent,
                ModelPropertyDisplayName = "Word Content",
                IsRequired = true,
                IsMeaningProperty = false
            },
            new CsvFieldMapping
            {
                ModelProperty = LexemeType,
                ModelPropertyDisplayName = "Lexeme Type",
                IsRequired = false,
                IsMeaningProperty = true
            },
            new CsvFieldMapping
            {
                ModelProperty = Level,
                ModelPropertyDisplayName = "Level",
                IsRequired = false,
                IsMeaningProperty = true
            },
            new CsvFieldMapping
            {
                ModelProperty = Definition,
                ModelPropertyDisplayName = "Definition",
                IsRequired = false,
                IsMeaningProperty = true
            }
        };
    }
}
