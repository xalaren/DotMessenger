using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.WebApi.Data.EntityTypeConfigurations
{
    public class ChatProfilesEntityTypeConfiguration : IEntityTypeConfiguration<ChatProfile>
    {
        public void Configure(EntityTypeBuilder<ChatProfile> builder)
        {
            builder
                .HasKey(chat => new { chat.ChatId, chat.AccountId });

            //builder
            //    .HasOne(chatProfile => chatProfile.Account)
            //    .WithMany(account => account.ChatProfiles)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
