using Ardalis.Specification;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.Core.Entities;
using VocabularyManager.Infrastructure.Data;
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
            services.AddScoped<IRepositoryBase<Vocabulary>, EfRepository<Vocabulary>>();
            services.AddScoped<IRepositoryBase<Word>, EfRepository<Word>>();
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddVocabularyValidator();
            services.AddWordValidator();
            services.AddWordDtoValidator();
            return services;
        }
        public static IServiceCollection AddStorageManagers(this IServiceCollection services)
        {
            services.AddVocabularyStorageManager();
            services.AddWordStorageManager();
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
            return services;
        }
    }
}
