namespace DotMessenger.Core.Model.Entities
{
    public class AppRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public bool IsMuted { get; set; }
        public bool CanDeleteUsers { get; set; }
        public bool CanMuteUsers { get; set; }
        public bool CanEditUsers { get; set; }
        public bool CanAssignRoles { get; set; }

    }
}
