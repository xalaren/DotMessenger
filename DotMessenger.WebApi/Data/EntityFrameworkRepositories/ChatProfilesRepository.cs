﻿using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.WebApi.Data.EntityFrameworkContexts;

namespace DotMessenger.WebApi.Data.EntityFrameworkRepositories
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

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
