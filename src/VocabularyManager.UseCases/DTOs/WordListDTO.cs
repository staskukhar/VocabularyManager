namespace VocabularyManager.UseCases.DTOs
{
    public class WordListDTO
    {
        public int? Id { get; init; } = 0;
        public string ListName { get; set; }
        public string? SourceUrl { get; set; }
        public List<WordDTO>? Words { get; set; } = new List<WordDTO>();
        public WordListDTO(string listName, string? sourceUrl) 
        {
            ListName = listName;
            SourceUrl = sourceUrl;
        }
        public WordListDTO() { }
    }
}
