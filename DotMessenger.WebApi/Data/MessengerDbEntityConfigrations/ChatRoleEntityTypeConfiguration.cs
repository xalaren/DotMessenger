using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.WebApi.Data.MessengerDbEntityConfigrations
{
    public class ChatRoleEntityTypeConfiguration : IEntityTypeConfiguration<ChatRole>
    {
        public void Configure(EntityTypeBuilder<ChatRole> builder)
        {
            builder
                .Property(chatRole => chatRole.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
