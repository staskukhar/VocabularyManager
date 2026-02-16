namespace VocabularyManager.BlazorApp.Models.Configurations
{
    public class HttpClientOptions
    {
        public const string SectionKey = "HttpClient";

        public string ApiBaseURL { get; set; } = string.Empty;

        public WordPathes WordTree { get; set; } = new();

        public WordParserPathes WordParserTree { get; set; } = new();

        public VocabularyPathes VocabularyTree { get; set; } = new();

        public MeaningPathes MeaningTree { get; set; } = new();

        public DashboardPathes DashboardTree { get; set; } = new();
    }

    public class DashboardPathes
    {
        public const string SectionKey = "Dashboard";

        public string PathPrefix { get; set; } = string.Empty;

        public string TopWords { get; set; } = string.Empty;

        public string TopVocabularies { get; set; } = string.Empty;
    }

    public class WordParserPathes
    {
        public string PathPrefix { get; set; } = string.Empty;

        public string ParseByUrl { get; set; } = string.Empty;
    }

    public class WordPathes
    {
        public const string SectionKey = "Word";

        public string PathPrefix { get; set; } = string.Empty;

        public string GetById { get; set; } = string.Empty;

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
