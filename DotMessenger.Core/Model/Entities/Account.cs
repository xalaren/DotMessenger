namespace DotMessenger.Core.Model.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
         
        public int? AppRoleId { get; set; }
        public AppRole? AppRole { get; set; }

        public ICollection<ChatProfile> ChatProfiles { get; set; } = new List<ChatProfile>();
    }
}
