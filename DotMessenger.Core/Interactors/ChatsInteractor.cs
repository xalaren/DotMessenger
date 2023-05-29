using System.Security.Principal;
using DotMessenger.Core.Interactors.Mappers;
using DotMessenger.Core.Mappers;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Exceptions;
using DotMessenger.Shared.Responses;

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
                    throw new BadRequestException("Заголовок был пустым");
                }

                if (accountId <= 0)
                {
                    throw new BadRequestException("Id аккаунта вышло за границы");
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
                    ErrorMessage = "Успешно создан чат",
                };
            }
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось создать чат",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" },
                };
            }
        }

        public Response Invite(int accountId, int chatId)
        {
            try
            {
                var account = accountsRepository.FindById(accountId);
                var chat = chatsRepository.FindById(chatId);
                if(account == null)
                {
                    throw new NotFoundException("Аккаунт не найден");
                }

                if(chat == null)
                {
                    throw new NotFoundException("Чат не найден");
                }


                var chatProfile = new ChatProfile()
                {
                    Account = account,
                    Chat = chat,
                };

                chatProfilesRepository.Create(chatProfile);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                };
            }
            catch(AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Невозможно пригласить пользователя",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response Quit(int requestAccountId, int accountId, int chatId)
        {
            try
            {
                var account = accountsRepository.FindById(accountId);
                var chat = chatsRepository.FindByIdIncludeProfiles(chatId);

                if(account == null)
                {
                    throw new NotFoundException("Пользователь не найден");
                }

                if(chat == null)
                {
                    throw new NotFoundException("Чат не найден");
                }

                var reqAccount = chat.ChatProfiles.FirstOrDefault(c => c.AccountId == requestAccountId);

                if(reqAccount == null)
                {
                    throw new NotFoundException("Пользователь не в чате");
                }

                if(reqAccount.AccountId != accountId)
                {
                    throw new NotAllowedException("Невозможно исключить пользователя");
                }

                chatProfilesRepository.Remove(reqAccount);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                };
            }
            catch(AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось выйти из чата",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response Kick(int accountId, int chatId)
        {
            try
            {
                var chatProfile = chatProfilesRepository.GetChatProfile(accountId, chatId);

                if(chatProfile == null)
                {
                    throw new NotFoundException("Профиль чата не найден");
                }

                chatProfilesRepository.Remove(chatProfile);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success",
                };
            }
            catch (AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось исключить пользователя",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response UpdateChat(int chatId, string title)
        {
            try
            {
                var chat = chatsRepository.FindById(chatId);

                if (chat == null)
                {
                    throw new NotFoundException("Чат не найден");
                }

                chat.Title = title;

                chatsRepository.Update(chat);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Чат успешно обновлен"
                };
            }
            catch(AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось обновить чат",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response<ChatDto[]> GetChats()
        {
            var result = chatsRepository.GetAllChats();

            if(result == null)
            {
                return new Response<ChatDto[]>()
                {
                    Error = false,
                    ErrorMessage = "Чаты не найдены"
                };
            }

            var mapped = result
                .Select(account => account.ToDto())
                .ToArray();

            return new()
            {
                Error = false,
                ErrorCode = 200,
                Value = mapped,
                ErrorMessage = "Успешно",
            };
        }

        public Response<ChatDto[]> GetAllUserChats(int userId)
        {
            var result = chatsRepository.GetAllUserChats(userId);

            if (result == null)
            {
                return new Response<ChatDto[]>()
                {
                    Error = false,
                    ErrorMessage = "Чаты не найдены"
                };
            }

            var mapped = result
                .Select(chat => chat.ToDto())
                .ToArray();

            return new()
            {
                Error = false,
                ErrorCode = 200,
                Value = mapped,
                ErrorMessage = "Успешно",
            };
        }

        public Response<ChatProfileDto[]> GetUsersFromChat(int chatId)
        {
            var result = chatProfilesRepository.GetChatProfiles(chatId);

            if(result == null)
            {
                return new Response<ChatProfileDto[]>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Нет профилей чатов"
                };
            }

            var mapped = result
                .Select(chatProfile => chatProfile.ToDto())
                .ToArray();

            return new Response<ChatProfileDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Успешно",
                Value = mapped,
            };
        }

        public Response DeleteChat(int chatId)
        {
            try
            {
                var chat = chatsRepository.FindById(chatId);

                if (chat == null)
                {
                    throw new NotFoundException("Чат не найден");
                }

                chatsRepository.Delete(chat);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Успешно",
                };
            }
            catch(AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось удалить чат",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }
    }
}