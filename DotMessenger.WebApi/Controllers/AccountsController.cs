using DotMessenger.Core.Interactors;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotMessenger.WebApi.Controllers
{
    [ApiController]
    [Route("api/AccountsController")]
    public class AccountsController : Controller
    {
        private readonly AccountsInteractor accountsInteractor;

        public AccountsController(AccountsInteractor accountsInteractor)
        {
            this.accountsInteractor = accountsInteractor;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public Response RegisterNewAccount(AccountDto accountDto)
        {
            return accountsInteractor.RegisterNewAccount(accountDto);
        }

        [Authorize]
        [HttpPost("update")]
        public Response UpdateAccount(AccountDto accountDto)
        {
            return accountsInteractor.UpdateAccount(accountDto);
        }

        [Authorize]
        [HttpGet("get-all")]
        public Response<AccountDto[]> GetAllAccounts()
        {
           return accountsInteractor.GetAllAccounts();
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public Response<SharedAccountDto> GetAccountById(int accountId)
        {
            return accountsInteractor.FindById(accountId);
        }
    }
}