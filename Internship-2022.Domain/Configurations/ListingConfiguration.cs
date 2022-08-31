using Internship_2022.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_2022.Domain.Configurations
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder
                .HasOne(entity => entity.User)
                .WithMany(entity => entity.Listings)
                .HasForeignKey(entity => entity.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(entity => entity.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.Description)
               .IsRequired();

            builder.Property(x => x.ShortDescription)
               .IsRequired();

            builder.Property(x => x.Location)
               .IsRequired();

            builder.Property(x => x.Price)
               .IsRequired();

            builder.Property(x => x.Author)
               .IsRequired();

            builder.Property(x => x.ApprovedBy);

            builder.Property(x => x.Status)
               .IsRequired();

            builder.Property(x => x.Images)
               .IsRequired();

            builder.Property(x => x.Category)
               .IsRequired();

            builder.Property(x => x.ViewCounter)
               .IsRequired();

            builder.Property(x => x.CreatedUtc)
               .IsRequired();

            builder.Property(x => x.UpdatedUtc)
               .IsRequired();

        }
    }
}