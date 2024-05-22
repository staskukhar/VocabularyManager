using Microsoft.Extensions.DependencyInjection;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.Parsers;
using VocabularyManager.UseCases.Services.StoreManagers;

namespace VocabularyManager.UseCases.Services.DIExtensions
{
    public static class ServicesInjectingExtensions
    {
        public static IServiceCollection AddOxfordParser(this IServiceCollection services) 
        {  
            return services.AddScoped<IWordParser<WordDTO>, OxfordDictionaryParser>();
        }
        public static IServiceCollection AddVocabularyStorageManager(this IServiceCollection services)
        {
            return services.AddScoped<IVocabularyStorageManager, VocabularyStorageManager>();
        }
        public static IServiceCollection AddWordStorageManager(this IServiceCollection services)
        {
            return services.AddScoped<IWordStorageManager, WordStorageManager>();
        }
    }
}
