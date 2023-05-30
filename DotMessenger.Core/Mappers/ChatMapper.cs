using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Interactors.Mappers;

public static class ChatMapper
{
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
            CreatedAt = chat.CreatedAt.ToShortDateString(),
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
            CreatedAt = DateTime.Parse(chatDto.CreatedAt),
        };
    }
}