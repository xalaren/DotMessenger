using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
{
    public class ChatProfilesRepository : IChatProfilesRepository
    {
        private readonly AppDbContext context;

        public ChatProfilesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(ChatProfile chatProfile)
        {
            context.ChatProfiles.Add(chatProfile);
        }

        public ChatProfile? GetChatProfile(int accountId, int chatId)
        {
           return context.ChatProfiles.SingleOrDefault(chatProfile =>
                  chatProfile.AccountId == accountId &&
                  chatProfile.ChatId == chatId);
        }

        public ChatProfile[]? GetChatProfiles(int chatId)
        {
            return context.ChatProfiles
                .Include(x => x.Chat)
                .Include(x => x.Account)
                .Where(x => x.ChatId == chatId).ToArray();
        }

        public void Remove(ChatProfile chatProfile)
        {
            context.ChatProfiles.Remove(chatProfile);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
