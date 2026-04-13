using Microsoft.Extensions.Localization;
using VocabularyManager.BlazorApp.Resources;

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

        public static List<CsvFieldMapping> GetMappingFields(IStringLocalizer<SharedResource> localizer) => new()
        {
            new CsvFieldMapping
            {
                ModelProperty = WordContent,
                ModelPropertyDisplayName = localizer["Label_WordContent"],
                IsRequired = true,
                IsMeaningProperty = false
            },
            new CsvFieldMapping
            {
                ModelProperty = LexemeType,
                ModelPropertyDisplayName = localizer["Label_LexemeType"],
                IsRequired = false,
                IsMeaningProperty = true
            },
            new CsvFieldMapping
            {
                ModelProperty = Level,
                ModelPropertyDisplayName = localizer["Label_Level"],
                IsRequired = false,
                IsMeaningProperty = true
            },
            new CsvFieldMapping
            {
                ModelProperty = Definition,
                ModelPropertyDisplayName = localizer["Label_Definition"],
                IsRequired = false,
                IsMeaningProperty = true
            }
        };
    }
}
