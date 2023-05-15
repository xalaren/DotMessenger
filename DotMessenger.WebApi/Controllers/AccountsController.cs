using DotMessenger.Core.Interactors;
using DotMessenger.Core.Model.Entities;
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
        public IActionResult RegisterNewAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            accountsInteractor.RegisterNewAccount(account);
            return Ok();
        }
    }
}