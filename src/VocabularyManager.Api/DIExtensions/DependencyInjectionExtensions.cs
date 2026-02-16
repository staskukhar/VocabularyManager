using Ardalis.Specification;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Core.Entities;
using VocabularyManager.Infrastructure.Data;
using VocabularyManager.Infrastructure.Data.Repositories;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.UseCases.Services.DIExtensions;
using VocabularyManager.UseCases.Validators.DIExtensions;

namespace VocabularyManager.Api.DIExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddValidators();
            services.AddStorageManagers();
            services.AddSecondaryServices();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryBase<Vocabulary>, GenericRepository<Vocabulary>>();
            services.AddScoped<IRepositoryBase<Word>, WordRepository>();
            services.AddScoped<IRepositoryBase<Meaning>, GenericRepository<Meaning>>();
            services.AddScoped<IDashboardMetricsProvider, DashboardMetricsProvider>();
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
