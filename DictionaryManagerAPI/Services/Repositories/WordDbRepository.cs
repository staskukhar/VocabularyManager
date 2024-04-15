using DictionaryManager.Shared.Models.Data;
using DictionaryManagerAPI.Services.Data;

namespace DictionaryManagerAPI.Services.Repositories
{
    public class WordDbRepository : IWordRepository, IDisposable
    {
        private VocabularyContext _repositoryContext;
        public WordDbRepository(VocabularyContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public Word Add(Word word)
        {
            return _repositoryContext.Add(word).Entity;
        }

        public Word Delete(Word word)
        {
            return _repositoryContext.Remove(word).Entity;
        }

        public void DeleteRange(IEnumerable<Word> words)
        {
            _repositoryContext.RemoveRange(words);
        }

        public IEnumerable<Word> GetAll()
        {
            return _repositoryContext.Words;
        }
        public Word GetById(int id)
        {
            return _repositoryContext.Words.Single(w => w.Id == id);
        }

        public Word Update(Word word)
        {
            return _repositoryContext.Update(word).Entity;
        }
        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
        public void Dispose()
        {
            _repositoryContext.Dispose();
        }
    }
}
