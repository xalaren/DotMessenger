using System.Security.Principal;
using System;

namespace DotMessenger.Shared.DataTransferObjects
{
    public class ChatProfileDto
    {
        public int ChatId { get; set; }
        public int AccountId { get; set; }
        public int ChatRoleId { get; set; }
    }
}
