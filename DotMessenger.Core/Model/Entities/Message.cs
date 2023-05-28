namespace DotMessenger.Core.Model.Entities;

public class Message
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime SendDate { get; set; }

    public ChatProfile? Sender { get; set; }
    public Chat Chat { get; set; } = null!;
}