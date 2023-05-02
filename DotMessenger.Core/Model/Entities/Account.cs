using DotMessenger.Core.Model.Abstract;

namespace DotMessenger.Core.Model.Entities
{
    public class Account : IUser, IPerson
    {
        private int id;
        private string name = string.Empty;
        private string lastname = string.Empty;
        private string phoneNumber = string.Empty;
        private string nickname = string.Empty;
        private string password = string.Empty;
        private string email = string.Empty;
        private DateTimeOffset birthDate;
        private int age;
        private List<Chat> chatList = new List<Chat>();

        public int Id
        {
            get => id;
            set
            {
                if (value < 0) return;
                id = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                name = value;
            }
        }

        public string Lastname
        {
            get => lastname;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                lastname = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                phoneNumber = value;
            }
        }

        public string Nickname
        {
            get => nickname;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                nickname = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                password = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                email = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 0) return;
                age = value;
            }
        }

        public DateTimeOffset BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
            }
        }

        public List<Chat> ChatList
        {
            get => chatList;
            set
            {
                if(value == null) return;
                chatList = value;
            }
        }
    }
}
