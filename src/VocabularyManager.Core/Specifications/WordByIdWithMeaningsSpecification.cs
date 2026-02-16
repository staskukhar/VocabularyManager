using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class WordByIdWithMeaningsSpecification : Specification<Word>
    {
        public WordByIdWithMeaningsSpecification(int wordId)
        {
            Query
                .Where(w => w.Id == wordId)
                .Include(w => w.Meanings);
        }
    }
}
