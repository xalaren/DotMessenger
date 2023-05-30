using DotMessenger.Core.Interactors;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("create")]
        public Response CreateChat(int accountId, string title)
        {
            return chatsInteractor.CreateChat(accountId, title);
        }

        [Authorize]
        [HttpPost("invite")]
        public Response Invite(int accountId, int chatId)
        {
            return chatsInteractor.Invite(accountId, chatId);
        }

        [Authorize]
        [HttpPost("quit")]
        public Response Quit(int requestAccountId, int accountId, int chatId)
        {
            return chatsInteractor.Quit(requestAccountId, accountId, chatId);
        }

        [Authorize]
        [HttpPost("kick")]
        public Response Kick(int accountId, int chatId)
        {
            return chatsInteractor.Kick(accountId, chatId);
        }

        [Authorize]
        [HttpPost("update")]
        public Response UpdateChat(int chatId, string title)
        {
            return chatsInteractor.UpdateChat(chatId, title);
        }

        [Authorize]
        [HttpGet("getAll")]
        public Response GetAllChats()
        {
            return chatsInteractor.GetChats();
        }

        [Authorize]
        [HttpGet("getChatsFromUser")]
        public Response GetAllUserChats(int userId)
        {
            return chatsInteractor.GetAllUserChats(userId);
        }

        [Authorize]
        [HttpGet("getUsers")]
        public Response<ChatProfileDto[]> GetUsers(int chatId)
        {
            return chatsInteractor.GetUsersFromChat(chatId);
        }

        [Authorize]
        [HttpDelete("delete-chat")]
        public Response DeleteChat(int chatId)
        {
            return chatsInteractor.DeleteChat(chatId);
        }
    }
}
