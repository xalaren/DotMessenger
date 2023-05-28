﻿using DotMessenger.Core.Interactors;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DotMessenger.WebApi.Controllers
{
    [ApiController]
    [Route("api/MessagesController")]
    public class MessagesController : Controller
    {
        private readonly MessagesInteractor messagesInteractor;

        public MessagesController(MessagesInteractor messagesInteractor)
        {
            this.messagesInteractor = messagesInteractor;
        }

        [HttpPost("create")]
        public Response SendMessage(MessageDto messageDto)
        {
            return messagesInteractor.SendMessage(messageDto);
        }

        [HttpDelete("delete")]
        public Response DeleteMessage(int messageId)
        {
            return messagesInteractor.Remove(messageId);
        }

        [HttpDelete("deleteRequest")]
        public Response RequestDeleteMessage(int accountId, int messageId)
        {
            return messagesInteractor.RemoveRequest(accountId, messageId);
        }


        [HttpGet("getFromChat")]
        public Response GetAllFromChat(int chatId)
        {
            return messagesInteractor.GetAllFromChat(chatId);
        }

        [HttpGet("getFromAccount")]
        public Response SendMessage(int accountId, int chatId)
        {
            return messagesInteractor.GetByAccountInChat(accountId, chatId);
        }
    }
}
