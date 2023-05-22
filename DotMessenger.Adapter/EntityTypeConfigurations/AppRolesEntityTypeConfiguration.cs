using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.Adapter.EntityTypeConfigurations
{
    public class AppRolesEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(appRole => appRole.Name)
                .IsRequired();
        }
    }
}