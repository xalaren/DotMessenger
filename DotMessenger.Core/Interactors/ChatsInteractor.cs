using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class ChatsInteractor
    {
        private readonly IChatsRepository chatsRepository;
        private readonly IChatProfilesRepository chatProfilesRepository;
        private readonly IAccountsRepository accountsRepository;

        public ChatsInteractor(
            IChatsRepository chatsRepository,
            IChatProfilesRepository chatProfilesRepository,
            IAccountsRepository accountsRepository)
        {

            if (chatsRepository == null || chatProfilesRepository == null || accountsRepository == null)
            {
                throw new ArgumentNullException("One of the repositories was null");
            }

            this.chatsRepository = chatsRepository;
            this.chatProfilesRepository = chatProfilesRepository;
            this.accountsRepository = accountsRepository;
        }

        public void CreateChat(int accountId, string title)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Title was null", nameof(title));
            }

            if(accountId <= 0)
            {
                throw new IndexOutOfRangeException("Account id was out of range");
            }

            var chat = new Chat()
            {
                Title = title,
                CreatedAt = DateTime.Now,
            };

            chatsRepository.Create(chat);

            var chatProfile = new ChatProfile()
            {
                Account = accountsRepository.FindById(accountId),
                Chat = chat,
            };

            chatProfilesRepository.Create(chatProfile);

            chatsRepository.Save();
            chatProfilesRepository.Save();

        }
    }
}
