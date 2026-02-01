namespace VocabularyManager.BlazorApp.Models.Configurations
{
    public class HttpClientOptions
    {
        public const string SectionKey = "HttpClient";
        public string ApiBaseURL { get; set; } = string.Empty;
        public WordPathes WordTree { get; set; } = new();
        public VocabularyPathes VocabularyTree { get; set; } = new();
        public MeaningPathes MeaningTree { get; set; } = new();
    }

    public class WordPathes
    {
        public const string SectionKey = "Word";
        public string PathPrefix { get; set; } = string.Empty;
        public string Get { get; set; } = string.Empty;
        public string Add { get; set; } = string.Empty;
        public string Delete { get; set; } = string.Empty;
        public string Update { get; set; } = string.Empty;
    }

    public class VocabularyPathes
    {
        public const string SectionKey = "Vocabulary";
        public string PathPrefix { get; set; } = string.Empty;
        public string Create { get; set; } = string.Empty;
        public string GetVocabularies { get; set; } = string.Empty;
        public string GetById { get; set; } = string.Empty;
        public string Update { get; set; } = string.Empty;
        public string Delete { get; set; } = string.Empty;
    }

    public class MeaningPathes
    {
        public const string SectionKey = "Meaning";
        public string PathPrefix { get; set; } = string.Empty;
        public string Add { get; set; } = string.Empty;
        public string Delete { get; set; } = string.Empty;
        public string Update { get; set; } = string.Empty;
    }
}
