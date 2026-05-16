using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Interfaces
{
    public interface IAnkiExportService
    {
        Task<AnkiExportResult?> ExportAsync(int vocabularyId, CancellationToken ct = default);
    }
}
