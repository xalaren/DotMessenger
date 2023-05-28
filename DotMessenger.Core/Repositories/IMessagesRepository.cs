using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IMessagesRepository
    {
        void Create(Message message);
        void Update(Message message);
        void Delete(Message message);
        Message? FindById(int messageId);
        Message? FindByIdIncludeSender(int messageId);
        Message[]? GetMessagesFromChat(int chatId);
        Message[]? GetMessagesByAccountInChat(int accountId, int chatId);
        Message[]? GetAllMessages();
    }
}
