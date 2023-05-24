using System;
using DotMessenger.Core.Interactors.Mappers;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Exceptions;
using DotMessenger.Shared.Responses;

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

            if (unitOfWork == null)
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
                if (!CheckForNullStrings(accountDto.Nickname, accountDto.Password, accountDto.Name,
                        accountDto.Lastname))
                {
                    throw new BadRequestException("Not all required fields were filled");
                }

                if (accountDto.BirthDate != null)
                {
                    accountDto.Age = CalculateAge(accountDto.BirthDate);
                }

                repository.Create(accountDto.ToEntity());
                unitOfWork.Commit();
            }
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot register account",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message:{exception.Message}" }
                };
            }

            return new()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
            };
        }

        public Response UpdateAccount(AccountDto accountDto)
        {
            try
            {
                if (accountDto == null || 
                    CheckForNullStrings(accountDto.Nickname, accountDto.Password,
                        accountDto.Name, accountDto.Lastname))
                {
                    throw new BadRequestException("Account data was null or empty");
                }

                var account = repository.FindById(accountDto.Id);

                if (account == null)
                {
                    throw new NotFoundException("Account not found");
                }
                
                repository.Update(accountDto.ToEntity());

                return new Response()
                {
                    Error = false,
                    ErrorMessage = "Success",
                };
            }
            catch (AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot update an account",
                    DetailedErrorInfo = new string[]
                    {
                        $"Type: {exception.Detail}",
                        $"Message: {exception.Message}"
                    },
                };
            }
        }

        public Response<AccountDto[]> GetAllAccounts()
        {
            var result = repository
                .GetAllAccounts()
                .Select(account => account.ToDto())
                .ToArray();

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
                if (string.IsNullOrWhiteSpace(value)) return true;
            }

            return false;
        }

        private int? CalculateAge(DateTime? birthDate)
        {
            if (birthDate == null)
            {
                return null;
            }

            var notNullBirthDate = birthDate.Value;
            var now = DateTime.Today;
            return now.Year - notNullBirthDate.Year - 1 +
                   ((now.Month > notNullBirthDate.Month ||
                     now.Month == notNullBirthDate.Month && now.Day >= notNullBirthDate.Day)
                       ? 1
                       : 0);
        }
    }
}