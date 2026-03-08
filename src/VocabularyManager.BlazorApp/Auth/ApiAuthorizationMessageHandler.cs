using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using VocabularyManager.BlazorApp.Models.Configurations;

namespace VocabularyManager.BlazorApp.Auth;

public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider tokenProvider,
        NavigationManager navigationManager,
        HttpClientOptions httpClientOptions)
        : base(tokenProvider, navigationManager)
    {
        Uri apiUri = new Uri(httpClientOptions.ApiBaseURL);
        string apiOrigin = $"{apiUri.Scheme}://{apiUri.Authority}";
        ConfigureHandler(authorizedUrls: [apiOrigin]);
    }
}
