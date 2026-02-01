using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class VocabularyWithWordsSpecification : Specification<Vocabulary>
    {
        public VocabularyWithWordsSpecification(int vocabularyId)
        {
            Query
                .Where(v => v.Id == vocabularyId)
                .Include(v => v.Words)
                    .ThenInclude(w => w.Meanings);
        }
    }
}
