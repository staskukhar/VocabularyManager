namespace VocabularyManager.UseCases.DTOs
{
    public class WordDTO
    {
        public int Id { get; init; }
        public string WordContent { get; set; } = string.Empty;
        public int VocabularyId { get; set; }
        public List<MeaningDTO> Meanings { get; set; } = [];

        public WordDTO() { }

        public WordDTO(string wordContent)
        {
            WordContent = wordContent;
        }

        public WordDTO(string wordContent, List<MeaningDTO> meanings)
        {
            WordContent = wordContent;
            Meanings = meanings;
        }
    }
}