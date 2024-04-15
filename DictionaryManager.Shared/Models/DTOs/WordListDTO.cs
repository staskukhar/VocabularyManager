using DictionaryManager.Shared.Models.Data;

namespace DictionaryManager.Shared.Models.DTOs
{
    public class WordListDTO
    {
        public int? Id { get; init; }
        public string ListName { get; set; }
        public string? SourceUrl { get; set; }
        public List<WordDTO>? Words { get; set; }
        public WordListDTO(string listName, string? sourceUrl) 
        {
            ListName = listName;
            SourceUrl = sourceUrl;
        }
        public WordListDTO() { }
    }
}
