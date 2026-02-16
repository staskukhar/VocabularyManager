using Microsoft.EntityFrameworkCore;
using VocabularyManager.Core.Entities;
using VocabularyManager.UseCases.Exceptions;

namespace VocabularyManager.Infrastructure.Data.Repositories;

public class WordRepository(VocabularyContext context)
    : GenericRepository<Word>(context)
{
    public override async Task UpdateAsync(Word entity, CancellationToken cancellationToken = default)
    {
        Word? existingWord = await _vocabularyContext.Words.Include(w => w.Meanings)
            .FirstOrDefaultAsync(w => w.Id == entity.Id && w.VocabularyId == entity.VocabularyId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Word), entity.Id);

        _vocabularyContext.Entry(existingWord).CurrentValues.SetValues(entity);

        IEnumerable<Meaning> existingMeanings = _vocabularyContext.Meanings
            .Where(m => m.WordId == entity.Id);

        foreach (Meaning m in existingMeanings)
        {
            if (!entity.Meanings.Any(em => em.Id == m.Id))
            {
                _vocabularyContext.Meanings.Remove(m);
            }
        }

        foreach (Meaning m in entity.Meanings)
        {
            Meaning? existing = await _vocabularyContext.Meanings.FindAsync(m.Id);
            if (existing != null)
            {
                _vocabularyContext.Entry(existing).CurrentValues.SetValues(m);
            }
            else
            {
                _vocabularyContext.Meanings.Add(m);
            }
        }
    }
}
