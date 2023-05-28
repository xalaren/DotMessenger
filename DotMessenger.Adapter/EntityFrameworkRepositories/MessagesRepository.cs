using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly AppDbContext context;

        public MessagesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(Message message)
        {
            context.Messages.Add(message);
        }

        public void Delete(Message message)
        {
            context.Remove(message);
        }

        public Message? FindById(int messageId)
        {
            return context.Messages.Find(messageId);
        }

        public Message? FindByIdIncludeSender(int messageId)
        {
            var message = FindById(messageId);
            if(message == null)
            {
                return null;
            }

            context.Entry(message)
                .Reference(message => message.Sender)
                .Load();

            return message;
        }

        public Message[]? GetMessagesByAccountInChat(int accountId, int chatId)
        {
            throw new();
        }

        public Message[]? GetMessagesFromChat(int chatId)
        {
            var chat = context.Chats.Find(chatId);

            if(chat == null)
            {
                return null;
            }

            context.Entry(chat)
                .Collection(x => x.Messages)
                .Load();

            foreach(var message in chat.Messages)
            {
                context.Entry(message)
                    .Reference(x => x.Sender)
                    .Load();

                context.Entry(message.Sender)
                    .Reference(x => x.Account)
                    .Load();
            }

            return chat.Messages.ToArray();
        }

        public Message[]? GetAllMessages()
        {
            return context.Messages.ToArray();
        }

        public void Update(Message message)
        {
            context.Messages.Update(message);
        }
    }
}
