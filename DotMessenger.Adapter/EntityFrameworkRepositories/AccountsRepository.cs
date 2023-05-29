using System.Diagnostics;
using System.Security.Principal;
using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AppDbContext context;

        public AccountsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Create(Account account)
        {
            context.Accounts.Add(account);
        }

        public Account? FindById(int accountId)
        {
            return context.Accounts.Find(accountId);
        }

        public Account? FindByAuthData(string nickname, string password)
        {
            return context.Accounts.FirstOrDefault(account => account.Nickname == nickname && account.Password == password);
        }

        public Account? FindByNickname(string nickname)
        {
            return context.Accounts.FirstOrDefault(account => account.Nickname == nickname);
        }

        public void Update(Account account)
        {
            context.Accounts.Update(account);
        }

        public void Remove(int accountId)
        {
            Account? account = context.Accounts.Find(accountId);

            if (account != null)
            {
                context.Remove(account);
            }
        }

        public Account[] GetAllAccounts()
        {
            return context.Accounts.ToArray();
        }
    }
}