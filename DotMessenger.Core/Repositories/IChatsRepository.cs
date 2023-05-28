using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IChatsRepository
    {
        void Create(Chat chat);
        void Update(Chat chat);
        void Delete(Chat chat);
        Chat? FindById(int chatId);
        Chat? FindByTitle(string title);
        Chat[]? GetAllChats();
        Chat[]? GetAllUserChats(int userId);
    }
}