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

        public void Delete(Chat chat)
        {
            context.Chats.Remove(chat);
        }

        public Chat? FindById(int chatId)
        {
            return context.Chats.Find(chatId);
        }

        public Chat? FindByTitle(string title)
        {
            return context.Chats.SingleOrDefault(chat => string.Equals(chat.Title, title));
        }

        public Chat[]? GetAllChats()
        {
            return context.Chats.ToArray();
        }

        public Chat[]? GetAllUserChats(int userId)
        {
            var account = context.Accounts.Find(userId);

            if (account == null)
            {
                return null;
            }

            context.Accounts.Entry(account)
                .Collection(acc => acc.ChatProfiles)
                .Load();

            var profiles = account.ChatProfiles;

            foreach (var profile in profiles)
            {
                context.ChatProfiles.Entry(profile)
                    .Reference(p => p.Chat)
                    .Load();
            }

            return profiles.Select(profile => profile.Chat).ToArray();
        }
        public void Update(Chat chat)
        {
            context.Entry(chat).State = EntityState.Modified;
        }
    }
}
