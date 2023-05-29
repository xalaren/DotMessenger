using System;
using System.Data.Common;
using System.Security.Cryptography;
using DotMessenger.Core.Encryption;
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
        private readonly AppRolesInteractor appRolesInteractor;
        private readonly IAccountsRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public AccountsInteractor(IAccountsRepository repository, IUnitOfWork unitOfWork, AppRolesInteractor appRolesInteractor)
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
            this.appRolesInteractor = appRolesInteractor;
        }

        public Response RegisterNewAccount(AccountDto accountDto)
        {
            try
            {
                if (CheckForNullStrings(accountDto.Nickname, accountDto.Password, accountDto.Name,
                        accountDto.Lastname))
                {
                    throw new BadRequestException("Not all required fields were filled");
                }

                var account = accountDto.ToEntity();
                repository.Create(account);
                unitOfWork.Commit();

                var response = appRolesInteractor.AssignUserRole(account.Id);

                if (response.Error)
                {
                    return response;
                }

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
            catch (ArgumentOutOfRangeException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = 400,
                    ErrorMessage = "Cannot register account",
                    DetailedErrorInfo = new string[] { $"Type: Bad request", $"Message:{exception.Message}" }
                };
            }

            return new()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
            };
        }

        public Response<AccountDto> Login(string nickname, string password)
        {
            try
            {
                password = SHA256Encryption.ComputeSha256Hash(password);

                var account = repository.FindByAuthData(nickname, password);

                if (account == null)
                {
                    throw new NotFoundException("Nickname or password was incorrect");
                }

                return new Response<AccountDto>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                    Value = account.ToDto(),
                };
            }
            catch (AppException exception)
            {
                return new Response<AccountDto>()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot login into account",
                    DetailedErrorInfo = new string[]
                    {
                        $"Type: {exception.Detail}",
                        $"Message: {exception.Message}"
                    },
                };
            }
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

                repository.Update(account.Assign(accountDto));
                unitOfWork.Commit();

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

        public Response<AccountDto> FindByNickname(string nickname)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nickname))
                {
                    throw new BadRequestException("Nickname was null or empty");
                }

                var account = repository.FindByNickname(nickname);

                if (account == null)
                {
                    throw new NotFoundException("Account not found");
                }

                return new Response<AccountDto>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                    Value = account.ToDto(),
                };
            }
            catch (AppException exception)
            {
                return new Response<AccountDto>()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot login into account",
                    DetailedErrorInfo = new string[]
                    {
                        $"Type: {exception.Detail}",
                        $"Message: {exception.Message}"
                    },
                };
            }
        }

        private bool CheckForNullStrings(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return true;
            }

            return false;
        }
    }
}