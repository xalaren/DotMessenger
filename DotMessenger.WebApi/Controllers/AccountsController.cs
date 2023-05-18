using DotMessenger.Core.Interactors;
using DotMessenger.Shared.DataTransferObjects;
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

        [HttpPost("register")]
        public Response RegisterNewAccount(AccountDto accountDto)
        {
            return accountsInteractor.RegisterNewAccount(accountDto);
        }

        [HttpGet("get-all")]
        public Response<IEnumerable<AccountDto>> GetAllAccounts()
        {
           return accountsInteractor.GetAllAccounts();
        }
    }
}