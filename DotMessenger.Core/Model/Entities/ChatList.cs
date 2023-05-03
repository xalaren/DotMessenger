namespace DotMessenger.Core.Model.Entities
{
    public class ChatList
    {
        private int userId { get; set; }
        private int chatId { get; set; }
        private int chatRoleId { get; set; }

        public int UserId
        {
            get => userId;
            set
            {
                if (value < 0) return;
                userId = value;
            }
        }

        public int ChatId
        {
            get => chatId;
            set
            {
                if (value < 0) return;
                chatId = value;
            }
        }

        public int ChatRoleId
        {
            get => chatRoleId;
            set
            {
                if (value < 0) return;
                chatRoleId = value;
            }
        }
    }
}
