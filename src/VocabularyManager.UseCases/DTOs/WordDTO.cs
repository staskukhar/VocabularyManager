namespace VocabularyManager.UseCases.DTOs
{
    public class WordDTO
    {
        public int? Id { get; init; } = 0;
        public string WordContent { get; set; }
        public string? LevelAttribute { get; set; }
        public string? Lexeme { get; set; }
        public string? Defenition { get; set; }
        public int VocabularyId { get; set; }
        public WordDTO(
            string wordContent,
            string? levelAttribute,
            string? lexeme,
            string? defenition
        )
        {
            WordContent = wordContent;
            LevelAttribute = levelAttribute;
            Lexeme = lexeme;
            Defenition = defenition;
        }
        public WordDTO() { }
    }   
}