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
        public Response CreateChat(int accountId, string title)
        {
            return chatsInteractor.CreateChat(accountId, title);
        }

        [HttpGet("get-all")]
        public Response GetAllChats()
        {
            return chatsInteractor.GetChats();
        }

        [HttpGet("get-all-user")]
        public Response GetAllUserChats(int userId)
        {
            return chatsInteractor.GetAllUserChats(userId);
        }
    }
}
