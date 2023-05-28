using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IChatProfilesRepository
    {
        void Create(ChatProfile chatProfile);
        ChatProfile? GetChatProfile(int accountId, int chatId);
        ChatProfile[]? GetChatProfiles(int chatId);
    }
}
