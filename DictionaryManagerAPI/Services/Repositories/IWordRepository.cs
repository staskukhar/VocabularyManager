using DictionaryManager.Shared.Models.Data;

namespace DictionaryManagerAPI.Services.Repositories
{
    public interface IWordRepository : IRepositoryBase<Word>, IRepositoryHandler
    {
        Word GetById(int id);
    }
}
