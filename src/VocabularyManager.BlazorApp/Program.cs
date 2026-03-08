using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using VocabularyManager.BlazorApp;
using VocabularyManager.BlazorApp.Auth;
using VocabularyManager.BlazorApp.DIExtensions;
using VocabularyManager.BlazorApp.Models.Configurations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClientOptions? httpOptions = builder.Configuration
    .GetSection(HttpClientOptions.SectionKey).Get<HttpClientOptions>();
if (httpOptions == null)
{
    throw new InvalidOperationException("HttpClient configuration is required.");
}

builder.Services.AddSingleton(httpOptions);

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration["Keycloak:Authority"];
    options.ProviderOptions.ClientId = builder.Configuration["Keycloak:ClientId"];
    options.ProviderOptions.ResponseType = "code";
});

builder.Services.AddScoped<ApiAuthorizationMessageHandler>();
builder.Services.AddHttpClient("API", client =>
    {
        client.BaseAddress = new Uri(httpOptions.ApiBaseURL);
    })
    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();
builder.Services.AddScoped(serviceProvider =>
    serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

builder.Services.AddFluentUIComponents();

builder.Services.InjectDependencies();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();