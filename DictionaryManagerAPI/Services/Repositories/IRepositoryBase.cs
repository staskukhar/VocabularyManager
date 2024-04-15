namespace DictionaryManagerAPI.Services.Repositories
{
    public interface IRepositoryBase<T>
    {
        T Add(T entity);
        T Delete(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll();
    }
}
