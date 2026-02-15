using VocabularyManager.BlazorApp.Models.Configurations;

namespace VocabularyManager.BlazorApp.Services
{
    public class HttpPathBuilder
    {
        private readonly HttpClientOptions _httpOptions;

        public HttpPathBuilder(HttpClientOptions httpOptions)
        {
            _httpOptions = httpOptions;
        }

        // Word endpoints
        public string AddWordEndpoint(int vocabularyId)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Add
                ),
                vocabularyId
            );
        }

        public string DeleteWordEndpoint(int wordId)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Delete
                ),
                wordId
            );
        }

        public string ParseListOfWordEndpoint(string url)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.WordTree.PathPrefix,
                    _httpOptions.WordTree.Get
                ),
                url
            );
        }

        public string UpdateWordEndpoint()
        {
            return string.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordTree.PathPrefix,
                _httpOptions.WordTree.Update
            );
        }

        // Vocabulary endpoints
        public string CreateVocabularyEndpoint()
        {
            return string.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.Create
            );
        }

        public string GetVocabulariesEndpoint(bool withWords = false)
        {
            string rawPath = string.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.GetVocabularies); // contains ?withWords={0}

            return string.Format(rawPath, withWords.ToString());
        }

        public string GetVocabularyByIdEndpoint(int vocabularyId)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.VocabularyTree.PathPrefix,
                    _httpOptions.VocabularyTree.GetById
                ),
                vocabularyId
            );
        }

        public string UpdateVocabularyEndpoint()
        {
            return string.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.VocabularyTree.PathPrefix,
                _httpOptions.VocabularyTree.Update
            );
        }

        public string DeleteVocabularyEndpoint(int id)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.VocabularyTree.PathPrefix,
                    _httpOptions.VocabularyTree.Delete
                ),
                id
            );
        }

        // Meaning endpoints
        public string AddMeaningEndpoint(int wordId)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.MeaningTree.PathPrefix,
                    _httpOptions.MeaningTree.Add
                ),
                wordId
            );
        }

        public string DeleteMeaningEndpoint(int meaningId)
        {
            return string.Format(
                string.Concat(
                    _httpOptions.ApiBaseURL,
                    _httpOptions.MeaningTree.PathPrefix,
                    _httpOptions.MeaningTree.Delete
                ),
                meaningId
            );
        }

        public string UpdateMeaningEndpoint()
        {
            return string.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.MeaningTree.PathPrefix,
                _httpOptions.MeaningTree.Update
            );
        }
    }
}
