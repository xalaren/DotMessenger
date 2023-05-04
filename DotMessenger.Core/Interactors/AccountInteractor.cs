using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AccountInteractor
    {
        private AccountRepository accountRepository = new AccountRepository();
        private MessageRepository messageRepository = new MessageRepository();
        private ChatRepository chatRepository = new ChatRepository();

        public void SendMessage(string text, int userId, int chatId)
        {
            var account = accountRepository.FindById(userId);
            var chat = chatRepository.FindById(chatId);

            if (account == null || chat == null) throw new ArgumentNullException();

            var message = new Message()
            {
                AccountId = userId,
                ChatId = chatId,
                Text = text
            };

            messageRepository.Add(message);
        }
    }
}
