using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.Adapter.EntityTypeConfigurations
{
    public class AccountsEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(account => account.Nickname)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(account => account.Password)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(account => account.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(account => account.Lastname)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}