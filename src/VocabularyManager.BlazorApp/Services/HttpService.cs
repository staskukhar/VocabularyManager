using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace VocabularyManager.BlazorApp.Services
{
    public class HttpService
    {
        private const string ClientKey = "API";
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient Client => _httpClientFactory.CreateClient(ClientKey);

        public virtual async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            try
            {
                return await Client.GetAsync(requestUrl);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return default!;
            }
        }
        public virtual async Task<HttpResponseMessage> PostWithJsonAsync<TRequest>(string requestUrl, TRequest data)
        {
            try
            {
                return await Client.PostAsJsonAsync(requestUrl, data);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return default!;
            }
        }
        public virtual async Task<HttpResponseMessage> DeleteAsync(string requestUrl)
        {
            try
            {
                return await Client.DeleteAsync(requestUrl);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return default!;
            }
        }
        public virtual async Task<HttpResponseMessage> PutAsync<TRequest>(string requestUrl, TRequest data)
        {
            try
            {
                return await Client.PutAsJsonAsync(requestUrl, data);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return default!;
            }
        }
    }
}
