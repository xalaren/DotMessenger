using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Mappers
{
    public static class ChatProfileMapper
    {
        public static ChatProfileDto ToDto(this ChatProfile chatProfile)
        {
            return new ChatProfileDto()
            {
                AccountId = chatProfile.AccountId,
                ChatId = chatProfile.ChatId,
                ChatRoleId = chatProfile.ChatRole == null ? 0 : chatProfile.ChatRole.Id,
            };
        }
    }
}
