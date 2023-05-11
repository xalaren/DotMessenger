using System.Runtime.CompilerServices;
using DotMessenger.Core.Interactors;
using DotMessenger.Core.Model.Entities;
using DotMessenger.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotMessenger.WebApi.Controllers
{
    [ApiController]
    [Route("api/AccountsController")]
    public class AccountsController : Controller
    {
        private readonly AccountInteractor accountInteractor;

        public AccountsController(AccountInteractor accountInteractor)
        {
            this.accountInteractor = accountInteractor;
        }

        [HttpPut("register")]
        public IActionResult RegisterNewAccount(string nickname, string password, string email,
             string name, string lastname, int day, int month, int year, string? phoneNumber = null)
        {
            DateTime birthDate = new DateTime(year, month, day);
            var account = new Account()
            {
                Nickname = nickname,
                Password = password,
                Email = email,
                Name = name,
                Lastname = lastname,
                BirthDate = birthDate,
                PhoneNumber = phoneNumber
            };

            accountInteractor.RegisterNewAccount(account);

            return Ok();
        }
    }
}
