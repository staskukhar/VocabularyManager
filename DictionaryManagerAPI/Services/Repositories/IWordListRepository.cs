using DictionaryManager.Shared.Models.Data;

namespace DictionaryManagerAPI.Services.Repositories
{
    public interface IWordListRepository : IRepositoryBase<WordList>, IRepositoryHandler
    {
        IEnumerable<WordList> GetAllWithRelations();
        WordList GetWithRelationsById(int id);
    }
}
