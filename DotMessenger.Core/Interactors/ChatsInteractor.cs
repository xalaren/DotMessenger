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
                    throw new BadRequestException("Title was null");
                }

                if (accountId <= 0)
                {
                    throw new BadRequestException("Account id was out of range");
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
            catch (AppException exception)
            {
                return new()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot create a new chat",
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
                    throw new NotFoundException("Account not found");
                }

                if(chat == null)
                {
                    throw new NotFoundException("Chat not found");
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
                    ErrorMessage = "Cannot invite user",
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
                    throw new NotFoundException("User not found");
                }

                if(chat == null)
                {
                    throw new NotFoundException("Chat not found");
                }

                var reqAccount = chat.ChatProfiles.FirstOrDefault(c => c.AccountId == requestAccountId);

                if(reqAccount == null)
                {
                    throw new NotFoundException("User is not in chat");
                }

                if(reqAccount.AccountId != accountId)
                {
                    throw new NotAllowedException("Cannot kick other users");
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
                    ErrorMessage = "Cannot quit from chat",
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
                    throw new NotFoundException("Chat profile not found");
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
                    ErrorMessage = "Cannot kick from chat",
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
                    throw new NotFoundException("Chat not found");
                }

                chat.Title = title;

                chatsRepository.Update(chat);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Success"
                };
            }
            catch(AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Cannot update chat",
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
                    ErrorMessage = "No such chats"
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
                ErrorMessage = "Success",
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
                    ErrorMessage = "No such chats"
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
                ErrorMessage = "Success",
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
                    ErrorMessage = "No chat profiles"
                };
            }

            var mapped = result
                .Select(chatProfile => chatProfile.ToDto())
                .ToArray();

            return new Response<ChatProfileDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
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
                    throw new NotFoundException("Chat not found");
                }

                chatsRepository.Delete(chat);
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
                    ErrorMessage = "Cannot delete the chat",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }
    }
}