using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VocabularyManager.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IDisposable where T : class
    {
        VocabularyContext _vocabularyContext;
        public EfRepository(VocabularyContext vocabularyContext) : base(vocabularyContext) 
        {
            _vocabularyContext = vocabularyContext;
        }

        public void Dispose()
        {
            _vocabularyContext.Dispose();
        }
    }

}
