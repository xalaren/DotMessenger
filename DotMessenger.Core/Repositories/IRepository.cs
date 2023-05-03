namespace DotMessenger.Core.Repositories
{
    public interface IRepository<T> where T : class 
    {
        T? FindById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
