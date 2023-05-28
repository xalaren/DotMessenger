using DotMessenger.Core.Interactors;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Responses;
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

        [HttpPost("invite")]
        public Response Invite(int accountId, int chatId)
        {
            return chatsInteractor.Invite(accountId, chatId);
        }

        [HttpPost("update")]
        public Response UpdateChat(int chatId, string title)
        {
            return chatsInteractor.UpdateChat(chatId, title);
        }

        [HttpGet("getAll")]
        public Response GetAllChats()
        {
            return chatsInteractor.GetChats();
        }

        [HttpGet("getChatsFromUser")]
        public Response GetAllUserChats(int userId)
        {
            return chatsInteractor.GetAllUserChats(userId);
        }

        [HttpGet("getUsers")]
        public Response<ChatProfileDto[]> GetUsers(int chatId)
        {
            return chatsInteractor.GetUsersFromChat(chatId);
        }

        [HttpDelete("delete-chat")]
        public Response DeleteChat(int chatId)
        {
            return chatsInteractor.DeleteChat(chatId);
        }
    }
}
