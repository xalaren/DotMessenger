using DotMessenger.Core.Interactors;
using DotMessenger.Core.Model.Entities;
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
        public IActionResult RegisterNewAccount(AccountDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            accountsInteractor.RegisterNewAccount(accountDto);
            return Ok();
        }

        [HttpGet("get-all")]
        public IActionResult GetAllAccounts()
        {
            var accounts = accountsInteractor.GetAllAccounts();
            return Ok(accounts);
        }
    }
}