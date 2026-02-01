using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class WordByContentAndVocabularySpecification : Specification<Word>
    {
        public WordByContentAndVocabularySpecification(string wordContent, int vocabularyId)
        {
            Query.Where(w => w.WordContent == wordContent && w.VocabularyId == vocabularyId);
        }
    }

    public class WordsByContentsAndVocabularySpecification : Specification<Word>
    {
        public WordsByContentsAndVocabularySpecification(IEnumerable<string> wordContents, int vocabularyId)
        {
            Query.Where(w => wordContents.Contains(w.WordContent) && w.VocabularyId == vocabularyId);
        }
    }
}
