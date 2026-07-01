using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using VocabularyManager.Core.Entities;
using VocabularyManager.Infrastructure.Data;
using VocabularyManager.Infrastructure.Data.Repositories;
using VocabularyManager.Infrastructure.Parsers;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryBase<Vocabulary>, GenericRepository<Vocabulary>>();
        services.AddScoped<IWordRepository, WordRepository>();
        services.AddScoped<IRepositoryBase<Meaning>, GenericRepository<Meaning>>();
        services.AddScoped<IDashboardMetricsProvider, DashboardMetricsProvider>();
        services.AddScoped<IWordParser<string>, SimpleOxfordDictionaryParser>();
        return services;
    }
}
