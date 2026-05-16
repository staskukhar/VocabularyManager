using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class VocabularyWithWordsAndMeaningsSpecification : Specification<Vocabulary>
    {
        public VocabularyWithWordsAndMeaningsSpecification(int vocabularyId)
        {
            Query
                .Where(v => v.Id == vocabularyId)
                .Include(v => v.Words)
                .ThenInclude(w => w.Meanings);
        }
    }
}
