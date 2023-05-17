using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AccountsInteractor
    {
        private readonly IAccountsRepository repository;

        public AccountsInteractor(IAccountsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("AccountsRepository example was null", nameof(repository));
            }
            
            this.repository = repository;
        }

        public void RegisterNewAccount(Account account)
        {
            if (!CheckForNullStrings(account.Nickname, account.Password, account.Name, account.Lastname))
            {
                throw new ArgumentNullException("Not all required fields were filled");
            }

            if (account.BirthDate != null)
            {
                account.Age = CalculateAge(account.BirthDate);
            }
            
            repository.Create(account);
            repository.Save();
        }
        
        private bool CheckForNullStrings(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return false;
            }

            return true;
        }

        private int CalculateAge(DateTime? birthDate)
        {
            var notNullBirthDate = birthDate.Value;
            var now = DateTime.Today;
            return now.Year - notNullBirthDate.Year - 1 +
                   ((now.Month > notNullBirthDate.Month || now.Month == notNullBirthDate.Month && now.Day >= notNullBirthDate.Day) ? 1 : 0);
        }
    }
}