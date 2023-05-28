using DotMessenger.Adapter.EntityTypeConfigurations;
using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.Adapter.EntityFrameworkContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatProfile> ChatProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatProfilesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MessagesEntityTypeConfiguration());
        }
    }
}