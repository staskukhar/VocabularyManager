using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using VocabularyManager.BlazorApp;
using VocabularyManager.BlazorApp.DIExtensions;
using VocabularyManager.BlazorApp.Models.Configurations;

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
builder.Services.AddScoped(sp =>
    new HttpClient()
    {
        BaseAddress = new Uri(httpOptions.ApiBaseURL)
    }
);

builder.Services.AddFluentUIComponents();

builder.Services.InjectDependencies();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();