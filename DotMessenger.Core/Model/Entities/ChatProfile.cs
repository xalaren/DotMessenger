namespace DotMessenger.Core.Model.Entities;

public class ChatProfile
{
    public int ChatId { get; set; }
    public int AccountId { get; set; }
    
    public Chat? Chat { get; set; }
    public Account? Account { get; set; }
}