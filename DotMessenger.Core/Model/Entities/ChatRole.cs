namespace DotMessenger.Core.Model.Entities
{
    public class ChatRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool CanDeleteMessages { get; set; }
        public bool CanSendMessages { get; set; }
        public bool CanKickUsers { get; set; }
    }
}
