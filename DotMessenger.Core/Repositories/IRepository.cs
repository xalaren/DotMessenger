namespace DotMessenger.Core.Repositories
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T FindById(int id);
        void Add(T entity);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
