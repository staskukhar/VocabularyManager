using DictionaryManager.Shared.Models.DTOs;
using DictionaryManagerApp;
using DictionaryManagerApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.Configuration.GetSection("URIStrings")["DictionaryManagerApi"])
});
builder.Services.AddScoped<IDictionaryRepository<WordListDTO, WordDTO>, DictionaryRepository>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
