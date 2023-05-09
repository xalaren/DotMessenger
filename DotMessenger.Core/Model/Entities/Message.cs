namespace DotMessenger.Core.Model.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
