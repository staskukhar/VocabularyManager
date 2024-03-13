namespace DictionaryManager.Shared.Models.DTOs
{
    public record class ParsingWordDTO
    {
        public string OriginWord { get; set; }
        public string LexemeType { get; init; }
        public string LevelAttribute { get; init; }
        public string[] AudioLinks { get; init; }
        public ParsingWordDTO(string originWord, string lexemeType, string levelAttribute, string[] audioLinks)
        {
            OriginWord = originWord;
            LexemeType = lexemeType;
            LevelAttribute = levelAttribute;
            AudioLinks = audioLinks;
        }
    }
}