using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.WebApi.Data
{
    public class MessengerDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRole> ChatRoles { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserList> UserLists { get; set; }

        public MessengerDbContext(DbContextOptions options) : base(options) { }
    }
}
