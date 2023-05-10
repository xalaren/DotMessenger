using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        public List<Account> Accounts { get; set; } = new List<Account>();

        public void Add(Account entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to add empty account", nameof(entity));

            Accounts.Add(entity);
        }

        public void Delete(Account entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to remove an empty chat", nameof(entity));
            Accounts.Remove(entity);
        }

        public Account? FindById(int id)
        {
            return Accounts.SingleOrDefault(account => account.Id == id);
        }

        public IEnumerable<Account> GetAll()
        {
            foreach (var account in Accounts)
            {
                yield return account;
            }
        }

        public void Update(Account entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to update to empty account", nameof(entity));

            var foundAccount = FindById(entity.Id);

            if (foundAccount == null) return;

            foundAccount = entity;
        }
    }
}
