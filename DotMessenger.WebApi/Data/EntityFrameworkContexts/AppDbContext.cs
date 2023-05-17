using DotMessenger.Core.Model.Entities;
using DotMessenger.WebApi.Data.EntityTypeConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.WebApi.Data.EntityFrameworkContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatProfile> ChatProfiles { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatProfilesEntityTypeConfiguration());
        }
    }
}