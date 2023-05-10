using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.WebApi.Data.MessengerDbEntityConfigrations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .Property(account => account.Nickname)
                .HasMaxLength(40)
                .IsRequired();

            builder
                .Property(account => account.Password)
                .HasMaxLength(40)
                .IsRequired();

            builder
                .Property(account => account.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(account => account.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(account => account.Lastname)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(account => account.PhoneNumber)
                .HasMaxLength(20);

            builder
                .Property(account => account.BirthDate)
                .IsRequired();
        }
    }
}
