using Ardalis.Specification.EntityFrameworkCore;

namespace VocabularyManager.Infrastructure.Data.Repositories
{
    public class GenericRepository<T>
        : RepositoryBase<T>, IDisposable where T : class
    {
        protected VocabularyContext _vocabularyContext;
        public GenericRepository(VocabularyContext vocabularyContext) : base(vocabularyContext) 
        {
            _vocabularyContext = vocabularyContext;
        }

        public void Dispose()
        {
            _vocabularyContext.Dispose();
        }
    }

}
