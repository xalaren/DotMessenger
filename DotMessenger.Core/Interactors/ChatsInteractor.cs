using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Interactors
{
    public class ChatsInteractor
    {
        private readonly IChatsRepository chatsRepository;
        private readonly IChatProfilesRepository chatProfilesRepository;
        private readonly IAccountsRepository accountsRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChatsInteractor(
            IChatsRepository chatsRepository,
            IChatProfilesRepository chatProfilesRepository,
            IAccountsRepository accountsRepository,
            IUnitOfWork unitOfWork)
        {

            if (chatsRepository == null || chatProfilesRepository == null || accountsRepository == null)
            {
                throw new ArgumentNullException("One of the repositories was null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work example was null", nameof(unitOfWork));
            }

            this.chatsRepository = chatsRepository;
            this.chatProfilesRepository = chatProfilesRepository;
            this.accountsRepository = accountsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Response CreateChat(int accountId, string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new ArgumentNullException("Title was null", nameof(title));
                }

                if (accountId <= 0)
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
                    Account = accountsRepository.FindById(accountId)!,
                    Chat = chat,
                };

                chatProfilesRepository.Create(chatProfile);

                unitOfWork.Commit();

                return new()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                };
            }
            catch (Exception exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = 403,
                    ErrorMessage = "Cannot create a new chat",
                    DetailedErrorInfo = new string[] { "Bad Request", $"Message: {exception.Message}" },
                };
            }
        }

        public Response<IEnumerable<ChatDto>> GetChats()
        {
            var result = chatsRepository.GetAllChats().Select(account => account.ToDto());

            return new()
            {
                Error = false,
                ErrorCode = 200,
                Value = result,
                ErrorMessage = "Success",
            };
        }

        public Response<IEnumerable<ChatDto>> GetAllUserChats(int userId)
        {
            try
            {
                var result = chatsRepository.GetAllUserChats(userId).Select(chat => chat.ToDto());

                return new()
                {
                    Error = false,
                    ErrorCode = 200,
                    Value = result,
                    ErrorMessage = "Success",
                };
            }
            catch (Exception exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = 404,
                    ErrorMessage = "Could not find user",
                    DetailedErrorInfo = new string[] { "Not Found", $"Message: {exception.Message}" }
                };
            }
        }
    }
}
