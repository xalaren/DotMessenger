namespace DotMessenger.Shared.DataTransferObjects
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string SenderName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime? SendDate { get; set; } 
    }
}
