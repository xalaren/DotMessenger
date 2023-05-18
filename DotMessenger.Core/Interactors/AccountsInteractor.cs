using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Interactors
{
    public class AccountsInteractor
    {
        private readonly IAccountsRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public AccountsInteractor(IAccountsRepository repository, IUnitOfWork unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("AccountsRepository example was null", nameof(repository));
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work example was null", nameof(unitOfWork));
            }
            
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void RegisterNewAccount(AccountDto accountDto)
        {
            if (!CheckForNullStrings(accountDto.Nickname, accountDto.Password, accountDto.Name, accountDto.Lastname))
            {
                throw new ArgumentNullException("Not all required fields were filled");
            }

            if (accountDto.BirthDate != null)
            {
                accountDto.Age = CalculateAge(accountDto.BirthDate);
            }
            
            repository.Create(accountDto.ToEntity());
            unitOfWork.Commit();
        }

        public IEnumerable<AccountDto> GetAllAccounts()
        {
            return repository.GetAllAccounts().Select(account => account.ToDto());
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