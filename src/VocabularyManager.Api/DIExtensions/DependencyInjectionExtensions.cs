using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Infrastructure;
using VocabularyManager.UseCases.Services.DIExtensions;
using VocabularyManager.UseCases.Validators.DIExtensions;

namespace VocabularyManager.Api.DIExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddValidators();
            services.AddStorageManagers();
            services.AddSecondaryServices();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddVocabularyValidator();
            services.AddWordValidator();
            services.AddMeaningValidator();
            services.AddWordDtoValidator();
            return services;
        }

        public static IServiceCollection AddStorageManagers(this IServiceCollection services)
        {
            services.AddVocabularyStorageManager();
            services.AddWordStorageManager();
            services.AddMeaningStorageManager();
            return services;
        }

        public static IServiceCollection AddSecondaryServices(this IServiceCollection services)
        {
            services.AddOxfordParser();
            return services;
        }

        public static IServiceCollection AddValidationFilters(this IServiceCollection services)
        {
            services.AddScoped<WordsValidationFilter>();
            services.AddScoped<WordValidationFilter>();
            services.AddScoped<VocabularyValidationFilter>();
            services.AddScoped<MeaningValidationFilter>();
            return services;
        }
    }
}
