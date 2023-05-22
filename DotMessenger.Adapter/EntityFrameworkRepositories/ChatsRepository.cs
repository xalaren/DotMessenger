using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
{
    public class ChatsRepository : IChatsRepository
    {
        private readonly AppDbContext context;

        public ChatsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(Chat chat)
        {
            context.Add(chat);
        }

        public void Delete(int chatId)
        {
            Chat? chat = context.Chats.Find(chatId);

            if (chat != null)
            {
                context.Remove(chat);
            }
        }

        public Chat? FindById(int chatId)
        {
            return context.Chats.Find(chatId);
        }

        public Chat? FindByTitle(string title)
        {
            return context.Chats.SingleOrDefault(chat => string.Equals(chat.Title, title));
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return context.Chats;
        }

        public IEnumerable<Chat> GetAllUserChats(int userId)
        {
            var chatProfiles = context.ChatProfiles.Where(chatProfile => chatProfile.AccountId == userId);

            if (chatProfiles.Count() == 0)
            {
                throw new ArgumentNullException("User was not found");
            }

            //TODO: переписать на правильное нахождение
            return context.Chats.Where(chat => chat.Id == 0);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Chat chat)
        {
            context.Entry(chat).State = EntityState.Modified;
        }
    }
}
