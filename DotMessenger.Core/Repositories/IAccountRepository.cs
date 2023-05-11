using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IAccountRepository
    {
        public void Add(Account account);
        public void Remove(int id);
        public Account? FindById(int id);
    }
}
