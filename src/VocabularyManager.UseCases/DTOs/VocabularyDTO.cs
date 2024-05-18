namespace VocabularyManager.UseCases.DTOs
{
    public class VocabularyDTO
    {
        public int? Id { get; init; } = 0;
        public string Name { get; set; }
        public string? SourceUrl { get; set; }
        public List<WordDTO>? Words { get; set; } = new List<WordDTO>();
        public VocabularyDTO(string name, string? sourceUrl) 
        {
            Name = name;
            SourceUrl = sourceUrl;
        }
        public VocabularyDTO() { }
    }
}
