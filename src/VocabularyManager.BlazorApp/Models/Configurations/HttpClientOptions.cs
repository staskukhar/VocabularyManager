namespace VocabularyManager.BlazorApp.Models.Configurations
{
    public class HttpClientOptions
    {
        public const string SectionKey = "HttpClient";
        public string ApiBaseURL { get; set; } = String.Empty;
        public WordPathes WordTree { get; set; }
        public VocabularyPathes VocabularyTree { get; set; }
        public HttpClientOptions()
        {

        }

    }
    public class WordPathes
    {
        public const string SectionKey = "Word";
        public string PathPrefix { get; set; } = String.Empty;
        public string Get { get; set; } = String.Empty;
        public string Add { get; set; } = String.Empty;
        public string Delete { get; set; } = String.Empty;
        public string Update { get; set; } = String.Empty;
        public WordPathes()
        {
            
        }
    }
    public class VocabularyPathes
    {
        public const string SectionKey = "Vocabulary";
        public string PathPrefix { get; set; } = String.Empty;
        public string Create { get; set; } = String.Empty;
        public string GetVocabularies { get; set; } = String.Empty;
        public string GetById { get; set; } = String.Empty;
        public string Update { get; set; } = String.Empty;
        public VocabularyPathes()
        {
            
        }
    }
}
