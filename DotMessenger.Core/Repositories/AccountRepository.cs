using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private List<Account> accounts = new List<Account>();

        public void Add(Account entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));
            accounts.Add(entity);
        }

        public void Delete(Account entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            accounts.Remove(entity);
        }

        public void Update(Account entity)
        {
            var foundAccount = FindById(entity.Id);
            if (foundAccount == null) throw new ArgumentNullException();

            foundAccount = entity;
        }

        public Account? FindById(int id)
        {
            return accounts.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Account> GetAll()
        {
            foreach(var account in accounts)
            {
                yield return account;
            }
        }
    }
}
