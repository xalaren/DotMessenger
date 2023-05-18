using System;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Interactors
{
    public static class DataTransferConverter
    {
        public static AccountDto ToDto(this Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException();
            }

            return new AccountDto()
            {
                Id = account.Id,
                Nickname = account.Nickname,
                Name = account.Name,
                Lastname = account.Lastname,
                Password = account.Password,
                Email = account.Email,
                Phone = account.Phone,
                BirthDate = account.BirthDate,
                Age = account.Age
            };
        }

        public static Account ToEntity(this AccountDto accountDto)
        {
            if (accountDto == null)
            {
                throw new ArgumentNullException();
            }

            return new Account()
            {
                Id = accountDto.Id,
                Nickname = accountDto.Nickname,
                Name = accountDto.Name,
                Lastname = accountDto.Lastname,
                Password = accountDto.Password,
                Email = accountDto.Email,
                Phone = accountDto.Phone,
                BirthDate = accountDto.BirthDate,
                Age = accountDto.Age
            };
        }

        public static ChatDto ToDto(this Chat chat)
        {
            if(chat == null)
            {
                throw new ArgumentNullException();
            }

            return new ChatDto()
            {
                Id = chat.Id,
                Title = chat.Title,
                CreatedAt = chat.CreatedAt
            };
        }

        public static Chat ToEntity(this ChatDto chatDto)
        {
            if(chatDto == null)
            {
                throw new ArgumentNullException();
            }

            return new Chat()
            {
                Id = chatDto.Id,
                Title = chatDto.Title,
                CreatedAt = chatDto.CreatedAt
            };
        }
    }
}
