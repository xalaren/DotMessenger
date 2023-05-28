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
                    throw new BadRequestException("Empty message");
                }

                var chatProfile = chatProfilesRepository.GetChatProfile(messageDto.SenderId, messageDto.ChatId);

                if (chatProfile == null)
                {
                    throw new BadRequestException("Chat or profiles not exists");
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
                    ErrorMessage = "Cannot send message",
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
                    throw new NotFoundException("Message not found");
                }

                messagesRepository.Delete(foundMessage);
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
                    ErrorMessage = "Cannot remove message",
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
                    throw new NotFoundException("Message not found");
                }

                if (sender == null)
                {
                    throw new NotFoundException("User not found");
                }

                if(foundMessage.Sender.AccountId != requestSenderId)
                {
                    throw new NotAllowedException("User is not sender");
                }
                messagesRepository.Delete(foundMessage);
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
                    ErrorMessage = "Cannot remove message",
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
                    ErrorMessage = "No messages",
                };
            } 

            var mapped = result
                .Select(message => message.ToDto())
                .ToArray();

            return new Response<MessageDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
                Value = mapped,
            };
        }

        public Response<Message[]> GetAllMessages()
        {
            return new Response<Message[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
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
                    ErrorMessage = "No messages",
                };
            }

            var mapped = result
                .Select(message => message.ToDto())
                .ToArray();

            return new Response<MessageDto[]>()
            {
                Error = false,
                ErrorCode = 200,
                ErrorMessage = "Success",
                Value = mapped,
            };
        }
    }
}
