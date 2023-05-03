namespace DotMessenger.Core.Model.Entities
{
    public class ChatRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool CanMessage { get; set; }
        public bool CanEditChat { get; set; }
        public bool CanDeleteChat { get; set; }
        public bool CanEditMessages { get; set; }
        public bool CanDeleteMessages { get; set; }
        public bool CanAssignChatRoles { get; set; }
        public bool CanMuteChatUsers { get; set; }
        public bool CanKick { get; set; }
    }
}
