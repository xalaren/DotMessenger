namespace DotMessenger.Core.Model.Entities
{
    public class Message
    {
        public int id;
        public int accountId;
        public int chatId;

        public string text = string.Empty;

        public int Id
        {
            get => id; 
            set
            {
                if (value < 0) return; 
                id = value;
            }
        }

        public int AccountId
        {
            get => accountId;
            set
            {
                if (value < 0) return;
                accountId = value;
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

        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                text = value;
            }
        }
    }
}
