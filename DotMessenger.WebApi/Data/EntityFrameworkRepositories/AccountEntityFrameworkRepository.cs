using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotMessenger.WebApi.Data.EntityFrameworkRepositories
{
    public class AccountEntityFrameworkRepository : IAccountRepository
    {
        private readonly MessengerDbContext context;

        public AccountEntityFrameworkRepository(MessengerDbContext context)
        {
            this.context = context;
        }

        public void Add(Account account)
        {
            if (account == null) throw new ArgumentNullException("Cannot add null account", nameof(account));
            context.Accounts.Add(account);
            context.SaveChanges();
        }

        public Account? FindById(int id)
        {
            return context.Accounts.Find(id);
        }

        public void Remove(int id)
        {
            var account = context.Accounts.SingleOrDefault(account => account.Id == id);

            if (account == null) return;

            context.Accounts.Remove(account);
        }
    }
}
