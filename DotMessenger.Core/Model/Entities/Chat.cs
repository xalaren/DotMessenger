namespace DotMessenger.Core.Model.Entities;

public class Chat
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<ChatProfile> ChatProfiles { get; set; } = new List<ChatProfile>();
}