namespace DotMessenger.Core.Model.Entities;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime SendDate { get; set; }
    
    public int SenderId { get; set; }
    public Account? Sender { get; set; }
}