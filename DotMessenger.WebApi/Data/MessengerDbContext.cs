using DotMessenger.Core.Model.Entities;
using DotMessenger.WebApi.Data.MessengerDbEntityConfigrations;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.WebApi.Data
{
    public class MessengerDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        //public DbSet<ChatUser> ChatUsers { get; set; }
        //public DbSet<Chat> Chats { get; set; }
        //public DbSet<Message> Messages { get; set; }
        //public DbSet<ChatRole> ChatRoles { get; set; }
        //public DbSet<AppRole> AppRoles { get; set; }
        //public DbSet<UserList> UserLists { get; set; }

        public MessengerDbContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ChatRoleEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new AppRoleEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserListEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ChatUserEntityTypeConfiguration());
        }
    }
}
