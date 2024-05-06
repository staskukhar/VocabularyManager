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

        public string AddWordEndpoint(int wordListId)
        {
            return String.Concat(
                _httpOptions.ApiBaseURL, 
                _httpOptions.WordTree.PathPrefix,
                _httpOptions.WordTree.Add,
                $"?wordListId={wordListId}"
            );
        }
        public string AddWordsEndpoint(int wordListId)
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordTree.PathPrefix,
                _httpOptions.WordTree.AddWords,
                $"?wordlistid={wordListId}"
            );
        }
        public string DeleteWordEndpoint(int wordId)
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordTree.PathPrefix,
                _httpOptions.WordTree.Delete,
                $"?id={wordId}"
            );
        }
        public string ParseListOfWordEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordListTree.PathPrefix,
                _httpOptions.WordListTree.GetWordList
            );
        }
        public string CreateWordListEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordListTree.PathPrefix,
                _httpOptions.WordListTree.Create
            );
        }
        public string GetWordListsEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordListTree.PathPrefix,
                _httpOptions.WordListTree.GetWordLists
            );
        }
        public string GetWordListByIdEndpoint(int wordListId)
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordListTree.PathPrefix,
                _httpOptions.WordListTree.GetById,
                $"?wordlistid={wordListId}"
            );
        }
        public string UpdateWordListEndpoint()
        {
            return String.Concat(
                _httpOptions.ApiBaseURL,
                _httpOptions.WordListTree.PathPrefix,
                _httpOptions.WordListTree.Update
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
    }
}
