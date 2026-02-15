using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class VocabularyWithWordsSpecification : Specification<Vocabulary>
    {
        public VocabularyWithWordsSpecification()
        {
            Query
                .Include(v => v.Words);
        }

        public VocabularyWithWordsSpecification(int vocabularyId)
        {
            Query
                .Where(v => v.Id == vocabularyId)
                .Include(v => v.Words);
        }
    }
}
