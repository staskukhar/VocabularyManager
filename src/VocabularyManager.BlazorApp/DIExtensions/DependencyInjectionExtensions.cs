using FluentValidation;
using VocabularyManager.BlazorApp.Interfaces;
using VocabularyManager.BlazorApp.Models.Views;
using VocabularyManager.BlazorApp.Services;
using VocabularyManager.BlazorApp.Validators;

namespace VocabularyManager.BlazorApp.DIExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services) 
        {
            services.AddScoped<HttpService, HttpService>();
            services.AddScoped<IVocabularyStateManager<VocabularyView>, VocabularyStateManager>();
            services.AddSingleton<HttpPathBuilder, HttpPathBuilder>();
            services.AddValidators();
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<VocabularyView>, VocabularyViewValidator>();
            services.AddScoped<IValidator<WordView>, WordViewValidator>();
            return services;
        }
    }
}
