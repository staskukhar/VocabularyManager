namespace VocabularyManager.BlazorApp.Models
{
    /// <summary>
    /// Represents a mapping between a Word model property and a CSV column.
    /// </summary>
    public class CsvFieldMapping
    {
        public string ModelProperty { get; set; } = string.Empty;
        public string ModelPropertyDisplayName { get; set; } = string.Empty;
        public string? CsvColumnName { get; set; }
        public bool IsRequired { get; set; }
    }

    /// <summary>
    /// Provides available Word model properties for CSV mapping.
    /// </summary>
    public static class WordModelProperties
    {
        public static List<CsvFieldMapping> GetMappingFields() => new()
        {
            new CsvFieldMapping 
            { 
                ModelProperty = nameof(Views.WordView.WordContent), 
                ModelPropertyDisplayName = "Word Content",
                IsRequired = true 
            },
            new CsvFieldMapping 
            { 
                ModelProperty = nameof(Views.WordView.Lexeme), 
                ModelPropertyDisplayName = "Lexeme",
                IsRequired = false 
            },
            new CsvFieldMapping 
            { 
                ModelProperty = nameof(Views.WordView.LevelAttribute), 
                ModelPropertyDisplayName = "Level",
                IsRequired = false 
            },
            new CsvFieldMapping 
            { 
                ModelProperty = nameof(Views.WordView.Defenition), 
                ModelPropertyDisplayName = "Definition",
                IsRequired = false 
            }
        };
    }
}
