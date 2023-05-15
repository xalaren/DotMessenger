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
                throw new ArgumentNullException("Repository example was null", nameof(repository));
            }
            
            this.repository = repository;
        }

        public void RegisterNewAccount(Account account)
        {
            if (!CheckForNullStrings(account.Nickname, account.Password, account.Name, account.Lastname))
            {
                throw new ArgumentNullException("Not all required fields were filled");
            }

            account.Age = CalculateAge(account.BirthDate);
            
            repository.Create(account);
        }
        
        private bool CheckForNullStrings(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return false;
            }

            return true;
        }

        private int CalculateAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                   ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }
    }
}