using DictionaryManager.Shared.Models.Data;
using DictionaryManagerAPI.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagerAPI.Services.Repositories
{
    public class WordListDbRepository : IWordListRepository, IDisposable
    {
        private VocabularyContext _repositoryContext;
        public WordListDbRepository(VocabularyContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public WordList Add(WordList wordList)
        {
            return _repositoryContext.Add(wordList).Entity;
        }

        public WordList Delete(WordList wordList)
        {
            return _repositoryContext.Remove(wordList).Entity;
        }
        public IEnumerable<WordList> GetAll()
        {
            return _repositoryContext.WordLists;
        }

        public IEnumerable<WordList> GetAllWithRelations()
        {
            return _repositoryContext.WordLists.Include(wl => wl.Words);
        }

        public WordList GetWithRelationsById(int id)
        {
            WordList wordList = _repositoryContext.WordLists
                    .Single(wl => wl.Id == id);

            _repositoryContext.Entry(wordList)
                .Collection(wl => wl.Words)
                .Load();

            return wordList;
        }
        public WordList Update(WordList wordList)
        {
            return _repositoryContext.Update(wordList).Entity;
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
