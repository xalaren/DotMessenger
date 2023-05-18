using System;
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

        public Response RegisterNewAccount(AccountDto accountDto)
        {
            try
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
            catch(Exception exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = 403,
                    ErrorMessage = "Cannot register account",
                    DetailedErrorInfo = new string[] { "Bad request", $"Message:{exception.Message}" }
                };
            }

            return new()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
            };
        }

        public Response<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var result = repository.GetAllAccounts().Select(account => account.ToDto());

            return new()
            {
                Error = false,
                ErrorCode = 200,
                Value = result,
                ErrorMessage = "Success",
            };
        }
        
        private bool CheckForNullStrings(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return false;
            }

            return true;
        }

        private int? CalculateAge(DateTime? birthDate)
        {
            if(birthDate == null)
            {
                return null;
            }

            var notNullBirthDate = birthDate.Value;
            var now = DateTime.Today;
            return now.Year - notNullBirthDate.Year - 1 +
                   ((now.Month > notNullBirthDate.Month || now.Month == notNullBirthDate.Month && now.Day >= notNullBirthDate.Day) ? 1 : 0);
        }
    }
}