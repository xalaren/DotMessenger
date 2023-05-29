using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IAccountsRepository
    {
        void Create(Account account);
        Account? FindById(int accountId);
        Account? FindByAuthData(string nickname, string password);
        void Update(Account account);
        void Remove(int accountId);
        Account[] GetAllAccounts();
    }
}