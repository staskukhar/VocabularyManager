using System.Net.Http.Json;

namespace VocabularyManager.BlazorApp.Services
{
    public class HttpService
    {
        HttpClient _httpClient;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public virtual async Task<HttpResponseMessage> Get(string requestUrl)
        {
            return await _httpClient.GetAsync(requestUrl);
        }
        public virtual  async Task<HttpResponseMessage> PostWithJson<TRequest>(string requestUrl, TRequest data)
        {
            return await _httpClient.PostAsJsonAsync(requestUrl, data);
        }
        public virtual async Task<HttpResponseMessage> Delete(string requestUrl)
        {
            return await _httpClient.DeleteAsync(requestUrl);
        }
        public virtual async Task<HttpResponseMessage> Put<TRequest>(string requestUrl, TRequest data)
        {
            return await _httpClient.PutAsJsonAsync(requestUrl, data);
        }
    }
}
