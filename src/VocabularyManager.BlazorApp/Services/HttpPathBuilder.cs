using VocabularyManager.BlazorApp.Models.Configurations;

namespace VocabularyManager.BlazorApp.Services
{
    public class HttpPathBuilder
    {
        private HttpClientOptions _httpOptions;
        public HttpPathBuilder(HttpClientOptions httpOptions) 
        {
            _httpOptions = httpOptions;
        }

        public string AddWordEndpoint(int vocabularyId)
        {
            return String.Format(
                String.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Add
                ),
                vocabularyId
            );
        }
        public string DeleteWordEndpoint(int wordId)
        {
            return String.Format(
                String.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Delete
                ),
                wordId
            );
        }
        public string ParseListOfWordEndpoint(string url)
        {
            return String.Format(
                String.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Get
                ),
                url
            );
        }
        public string CreateVocabularyEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.Create
            );
        }
        public string GetVocabularyEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.GetVocabularies
            );
        }
        public string GetVocabularytByIdEndpoint(int vocabularyId)
        {
            return String.Format(
                String.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.VocabularyTree.PathPrefix,
                    _httpOptions.VocabularyTree.GetById
                ),
                vocabularyId
            );
        }
        public string UpdateVocabularyEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.Update
            );
        }
        public string UpdateWordEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordTree.PathPrefix,
                _httpOptions.WordTree.Update
            );
        }
        public string DeleteVocabularyEndpoint(int id)
        {
            return String.Format(
                String.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.VocabularyTree.PathPrefix,
                    _httpOptions.VocabularyTree.Delete
                ),
                id
            );
        }
    }
}
