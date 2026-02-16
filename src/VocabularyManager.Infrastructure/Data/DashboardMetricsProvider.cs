using Microsoft.EntityFrameworkCore;
using VocabularyManager.Infrastructure.Data;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Infrastructure.Data
{
    public class DashboardMetricsProvider : IDashboardMetricsProvider
    {
        private readonly VocabularyContext _context;

        public DashboardMetricsProvider(VocabularyContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TopWordByDefinitionsDto>> GetTopWordsByDefinitionCountAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _context.Words
                .AsNoTracking()
                .Include(w => w.Vocabulary)
                .Select(w => new TopWordByDefinitionsDto
                {
                    WordId = w.Id,
                    WordContent = w.WordContent,
                    VocabularyName = w.Vocabulary != null ? w.Vocabulary.Name : string.Empty,
                    DefinitionCount = w.Meanings.Count
                })
                .OrderByDescending(dto => dto.DefinitionCount)
                .ThenBy(dto => dto.WordContent)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TopVocabularyByWordsDto>> GetTopVocabulariesByWordCountAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _context.Vocabularies
                .AsNoTracking()
                .Select(v => new TopVocabularyByWordsDto
                {
                    VocabularyId = v.Id,
                    VocabularyName = v.Name,
                    WordCount = v.Words.Count
                })
                .OrderByDescending(dto => dto.WordCount)
                .Take(count)
                .ToListAsync(cancellationToken);
        }
    }
}
