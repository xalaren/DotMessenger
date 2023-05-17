using DotMessenger.Core.Interactors;
using Microsoft.AspNetCore.Mvc;

namespace DotMessenger.WebApi.Controllers
{
    [ApiController]
    [Route("api/ChatsController")]
    public class ChatsController : Controller
    {
        private readonly ChatsInteractor chatsInteractor;

        public ChatsController(ChatsInteractor chatsInteractor)
        {
            this.chatsInteractor = chatsInteractor;
        }

        [HttpPost("create")]
        public IActionResult CreateChat(int accountId, string title)
        {
            chatsInteractor.CreateChat(accountId, title);

            return Ok();
        }
    }
}
