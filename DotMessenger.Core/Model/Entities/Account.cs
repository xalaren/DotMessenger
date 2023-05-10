using DotMessenger.Core.Model.Abstract;

namespace DotMessenger.Core.Model.Entities
{
    public class Account : IUser, IPerson
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }

        public int AppRoleId { get; set; }
        public int UserListId { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
        public AppRole AppRole { get; set; } = null!;
        public UserList UserList { get; set; } = null!;


    }
}
