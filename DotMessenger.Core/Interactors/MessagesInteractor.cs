using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotMessenger.Core.Mappers;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Exceptions;
using DotMessenger.Shared.Responses;

namespace DotMessenger.Core.Interactors
{
    public class MessagesInteractor
    {
        private readonly IAccountsRepository accountsRepository;
        private readonly IChatProfilesRepository chatProfilesRepository;
        private readonly IMessagesRepository messagesRepository;
        private readonly IUnitOfWork unitOfWork;

        public MessagesInteractor(
            IMessagesRepository messagesRepository,
            IChatProfilesRepository chatProfilesRepository,
            IAccountsRepository accountsRepository,
            IUnitOfWork unitOfWork)
        {
            this.messagesRepository = messagesRepository;
            this.chatProfilesRepository = chatProfilesRepository;
            this.unitOfWork = unitOfWork;
            this.accountsRepository = accountsRepository;
        }

        public Response SendMessage(MessageDto messageDto)
        {
            try
            {
                if (messageDto == null)
                {
                    throw new BadRequestException("Пустое сообщение");
                }

                var chatProfile = chatProfilesRepository.GetChatProfile(messageDto.SenderId, messageDto.ChatId);

                if (chatProfile == null)
                {
                    throw new BadRequestException("Чат или профиль не существуют");
                }

                var message = new Message()
                {
                    Sender = chatProfile,
                    ChatId = chatProfile.ChatId,
                    SendDate = DateTime.Now,
                    Text = messageDto.Text,
                };

                messagesRepository.Create(message);
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
                    ErrorMessage = "Не удалось отправить сообщение",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response Remove(int messageId)
        {
            try
            {
                var foundMessage = messagesRepository.FindById(messageId);

                if(foundMessage == null)
                {
                    throw new NotFoundException("Сообшение не найдено");
                }

                messagesRepository.Delete(foundMessage);
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
                    ErrorMessage = "Не удалось удалить сообщение",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response RemoveRequest(int requestSenderId, int messageId)
        {
            try
            {
                var foundMessage = messagesRepository.FindByIdIncludeSender(messageId);
                var sender = accountsRepository.FindById(requestSenderId);

                if (foundMessage == null)
                {
                    throw new NotFoundException("Сообщение не найдено");
                }

                if (sender == null)
                {
                    throw new NotFoundException("Пользователь не найден");
                }

                if(foundMessage.Sender.AccountId != requestSenderId)
                {
                    throw new NotAllowedException("Пользователь не был отправителем");
                }
                messagesRepository.Delete(foundMessage);
                unitOfWork.Commit();

                return new Response()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Успешно",
                };
            }
            catch (AppException exception)
            {
                return new Response()
                {
                    Error = true,
                    ErrorCode = exception.Code,
                    ErrorMessage = "Не удалось удалить сообщение",
                    DetailedErrorInfo = new string[] { $"Type: {exception.Detail}", $"Message: {exception.Message}" }
                };
            }
        }

        public Response<MessageDto[]> GetAllFromChat(int chatId)
        {
            var result = messagesRepository.GetMessagesFromChat(chatId);

            if (result == null)
            {
                return new Response<MessageDto[]>()
                {
                    Error = false,
                    ErrorCode = 202,
                    ErrorMessage = "Нет сообщений",
                };
            } 

            var mapped = result
                .Select(message => message.ToDto())
                .ToArray();

            return new Response<MessageDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Успешно",
                Value = mapped,
            };
        }

        public Response<Message[]> GetAllMessages()
        {
            return new Response<Message[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Успешно",
                Value = messagesRepository.GetAllMessages(),
            };
        }

        public Response<MessageDto[]> GetByAccountInChat(int accountId, int chatId)
        {
            var result = messagesRepository.GetMessagesByAccountInChat(accountId, chatId);

            if (result == null)
            {
                return new Response<MessageDto[]>()
                {
                    Error = false,
                    ErrorCode = 200,
                    ErrorMessage = "Нет сообщений",
                };
            }

            var mapped = result
                .Select(message => message.ToDto())
                .ToArray();

            return new Response<MessageDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Успешно",
                Value = mapped,
            };
        }
    }
}
