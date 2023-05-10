using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.WebApi.Data.MessengerDbEntityConfigrations
{
    public class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
                .Property(appRole => appRole.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
