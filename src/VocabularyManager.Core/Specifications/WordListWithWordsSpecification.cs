using Ardalis.Specification;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Core.Specifications
{
    public class WordListWithWordsSpecification : Specification<WordList>
    {
        public WordListWithWordsSpecification(int wordListId) 
        {
            Query
                .Where(wl => wl.Id == wordListId)
                .Include(wl => wl.Words);
        }
    }
}
