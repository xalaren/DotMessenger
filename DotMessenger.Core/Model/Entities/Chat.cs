namespace DotMessenger.Core.Model.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatUser> Members { get; set; } = new List<ChatUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>(); 
    }
}
