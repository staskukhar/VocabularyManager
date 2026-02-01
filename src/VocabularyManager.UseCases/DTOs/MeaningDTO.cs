namespace VocabularyManager.UseCases.DTOs
{
    public class MeaningDTO
    {
        public int Id { get; init; }
        public string? LexemeType { get; set; }
        public string? Definition { get; set; }
        public string? Level { get; set; }
        public int WordId { get; set; }

        public MeaningDTO() { }

        public MeaningDTO(string? lexemeType, string? definition, string? level)
        {
            LexemeType = lexemeType;
            Definition = definition;
            Level = level;
        }
    }
}
