namespace DotMessenger.Core.Model.Entities
{
    public class Chat
    {
        private int id;
        private int accountId;
        private int creatorId;

        private string title = string.Empty;

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
            }
        }

        public int CreatorId
        {
            get => creatorId;
            set
            {
                if (value < 0) return;
                creatorId = value;
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if(string.IsNullOrWhiteSpace(value)) return;
                title = value;
            }
        }

    }
}
