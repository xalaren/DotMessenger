using System.Xml.Linq;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.Exceptions;
using DotMessenger.Shared.Responses;

namespace DotMessenger.Core.Interactors
{
    public class AppRolesInteractor
    {
        private readonly IAccountsRepository accountsRepository;
        private readonly IAppRolesRepository appRolesRepository;
        private readonly IUnitOfWork unitOfWork;

        public AppRolesInteractor(IAppRolesRepository repository, IUnitOfWork unitOfWork, IAccountsRepository accountsRepository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("AppRolesRepository example was null", nameof(repository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work example was null", nameof(unitOfWork));
            }

            this.appRolesRepository = repository;
            this.unitOfWork = unitOfWork;
            this.accountsRepository = accountsRepository;
            CreateDefaultRoles();
        }

        private void CreateDefaultRoles()
        {
            var userRole = new AppRole()
            {
                Name = "user"
            };

            var adminRole = new AppRole()
            {
                Name = "admin"
            };

            if (appRolesRepository.FindByName(userRole.Name) == null)
            {
                appRolesRepository.Add(userRole);
            }

            if (appRolesRepository.FindByName(adminRole.Name) == null)
            {
                appRolesRepository.Add(adminRole);
            }

            unitOfWork.Commit();
        }

        public Response AssignUserRole(int accountId)
        {
            try
            {
                var account = accountsRepository.FindById(accountId);

                if (account == null)
                {
                    throw new NotFoundException("Account not found");
                }

                var role = appRolesRepository.FindByName("user");

                account.AppRole = role;

                accountsRepository.Update(account);

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                };
            }
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot assign role",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message:{exception.Message}" }
                };
            }
        }

        public Response<AppRole> FindById(int roleId)
        {
            return new Response<AppRole>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
                Value = appRolesRepository.FindById(roleId)
            };
        }

        public Response<AppRole> FindByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new BadRequestException("Name was null or empty");
                }
                return new Response<AppRole>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                    Value = appRolesRepository.FindByName(name)
                };
            }
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot find role",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message:{exception.Message}" }
                };
            }
        }

        public Response<AppRole> FindByAccount(int accountId)
        {
            try
            {
                var role = appRolesRepository.FindByAccount(accountId);

                if(role == null)
                {
                    throw new NotFoundException("Account not found");
                }
                return new Response<AppRole>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                    Value = role
                };
            }
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot find role",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message:{exception.Message}" }
                };
            }
        }
    }
}