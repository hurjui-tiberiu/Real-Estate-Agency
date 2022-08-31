using Internship_2022.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_2022.Domain.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
               .HasOne(entity => entity.Receiver)
               .WithOne(entity => entity.MessageReceiver)
               .HasForeignKey<Message>(entity => entity.ReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(entity => entity.Sender)
               .WithOne(entity => entity.MessageSender)
               .HasForeignKey<Message>(entity => entity.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
             .HasOne(entity => entity.Listing)
             .WithOne(entity => entity.Message)
             .HasForeignKey<Message>(entity => entity.ListingId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SenderId).
                IsRequired();

            builder.Property(x => x.ReceiverId).
                IsRequired();

            builder.Property(x => x.ListingId).
                IsRequired();

            builder.Property(x => x.CreatedAt).
                IsRequired();

            builder.Property(x => x.UpdatedAt).
                IsRequired();

            builder.Property(x => x.Content).
                IsRequired();

            builder.Property(x => x.ViewStatus); 
        }
    }
}
