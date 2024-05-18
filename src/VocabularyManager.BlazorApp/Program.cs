using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VocabularyManager.BlazorApp;
using VocabularyManager.BlazorApp.Interfaces;
using VocabularyManager.BlazorApp.Models.Configurations;
using VocabularyManager.BlazorApp.Models.Views;
using VocabularyManager.BlazorApp.Services;
using VocabularyManager.BlazorApp.Validators;

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
builder.Services.AddScoped<IVocabularyStateManager<VocabularyView>, VocabularyStateManager>();
builder.Services.AddScoped<IValidator<VocabularyView>, VocabularyViewValidator>();
builder.Services.AddScoped<IValidator<WordView>, WordViewValidator>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
