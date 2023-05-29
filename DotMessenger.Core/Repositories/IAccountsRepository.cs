using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IAccountsRepository
    {
        void Create(Account account);
        Account? FindById(int accountId);
        Account? FindByAuthData(string nickname, string password);
        Account? FindByNickname(string nickname);
        void Update(Account account);
        void Remove(int accountId);
        Account[] GetAllAccounts();
    }
}