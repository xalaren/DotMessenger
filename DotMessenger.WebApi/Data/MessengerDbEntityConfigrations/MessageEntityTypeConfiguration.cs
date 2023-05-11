using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.WebApi.Data.MessengerDbEntityConfigrations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {

            builder
                .Property(message => message.Text)
                .IsRequired()
                .HasMaxLength(4096);

            builder
                .Property(message => message.CreatedAt)
                .IsRequired();
        }
    }
}
