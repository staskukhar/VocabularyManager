using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class WordsByContentWithMeaningsSpecification : Specification<Word>
    {
        public WordsByContentWithMeaningsSpecification(string wordContent)
        {
            Query
                .Where(w => w.WordContent == wordContent)
                .Include(w => w.Meanings);
        }
    }
}
