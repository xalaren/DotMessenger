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
                    throw new BadRequestException("Не все обязательные поля заполнены");
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
                    ErrorMessage = "Невозможно зарегистрировать аккаунт",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message:{exception.Message}" }
                };
            }
            catch (ArgumentOutOfRangeException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = 400,
                    ErrorMessage = "Невозможно зарегистрировать аккаунт",
                    DetailedErrorInfo = new string[] { $"Type: Bad request", $"Message:{exception.Message}" }
                };
            }

            return new()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Аккаунт успешно зарегистрирован",
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
                    throw new NotFoundException("Никнейм или пароль неверные");
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
                    ErrorMessage = "Не удалось войти в аккаунт",
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
                    throw new BadRequestException("Данные об аккаунте были пустыми");
                }

                var account = repository.FindById(accountDto.Id);

                if (account == null)
                {
                    throw new NotFoundException("Аккаунт не найден");
                }

                repository.Update(account.Assign(accountDto));
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorMessage = "Аккаунт успешно обновлен",
                };
            }
            catch (AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось обновить аккаунт",
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

        public Response<SharedAccountDto> FindByNickname(string nickname)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nickname))
                {
                    throw new BadRequestException("Никнейм был пустым");
                }

                var account = repository.FindByNickname(nickname);

                if (account == null)
                {
                    throw new NotFoundException("Аккаунт не найден");
                }

                return new Response<SharedAccountDto>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                    Value = account.ToSharedDto(),
                };
            }
            catch (AppException exception)
            {
                return new Response<SharedAccountDto>()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Невозможно войти в аккаунт",
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