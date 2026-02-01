using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Validators.DIExtensions
{
    public static class ValidationInjectingExtensions
    {
        public static IServiceCollection AddVocabularyValidator(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<Vocabulary>, VocabularyValidator>();
        }

        public static IServiceCollection AddWordValidator(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<Word>, WordValidator>();
        }

        public static IServiceCollection AddMeaningValidator(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<Meaning>, MeaningValidator>();
        }

        public static IServiceCollection AddWordDtoValidator(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<WordDTO>, OxfordParsingWordDTOValidator>();
        }
    }
}
