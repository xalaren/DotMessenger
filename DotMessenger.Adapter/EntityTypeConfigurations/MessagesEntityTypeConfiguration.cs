using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotMessenger.Adapter.EntityTypeConfigurations
{
    public class MessagesEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            //builder
            //    .HasOne(message => message.Sender)
            //    .WithMany(sender => sender.Messages)
            //    .HasForeignKey(message => message.SenderId);
        }
    }
}
