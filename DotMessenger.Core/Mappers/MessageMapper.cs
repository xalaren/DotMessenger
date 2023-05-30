using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto ToDto(this Message message)
        {
            return new MessageDto()
            {
                Id = message.Id,
                ChatId = message.ChatId,
                SenderId = message.Sender.AccountId,
                SenderName = message.Sender.Account.Name,
                Text = message.Text,
                SendDate = message.SendDate,
            };
        }

        public static Message ToEntity(this MessageDto messageDto)
        {
            return new Message()
            {
                Id = messageDto.Id,
                Text = messageDto.Text,
            };
        }
    }
}
