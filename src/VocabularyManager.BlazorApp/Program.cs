using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VocabularyManager.BlazorApp;
using VocabularyManager.BlazorApp.Models.Configurations;
using VocabularyManager.BlazorApp.Services;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.UseCases.DTOs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClientOptions? httpOptions = builder.Configuration
    .GetSection(HttpClientOptions.SectionKey).Get<HttpClientOptions>();
if(httpOptions == null)
{
    throw new NullReferenceException("Configurations weren't set.");
}

builder.Services.AddSingleton( sg =>
    httpOptions!
);
builder.Services.AddSingleton<HttpPathBuilder, HttpPathBuilder>();

builder.Services.AddScoped( sp =>
    new HttpClient()
    {
        BaseAddress = new Uri(httpOptions.ApiBaseURL)
    }
);
builder.Services.AddScoped<HttpService, HttpService>();
builder.Services.AddScoped<IWordListStateManager<WordListDTO>, WordListStateManager>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
