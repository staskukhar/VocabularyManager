using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IDashboardMetricsProvider
    {
        Task<IReadOnlyList<TopWordByDefinitionsDto>> GetTopWordsByDefinitionCountAsync(int count, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TopVocabularyByWordsDto>> GetTopVocabulariesByWordCountAsync(int count, CancellationToken cancellationToken = default);
    }
}
