using Internship_2022.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Domain.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);

            builder
               .HasOne(entity => entity.User)
               .WithMany(entity => entity.Favorites)
               .HasForeignKey(entity => entity.UserId);

            builder
                .HasOne(entity => entity.Listing)
                .WithMany(entity => entity.Favorites)
                .HasForeignKey(entity => entity.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.UserId);

            builder.Property(x => x.ListingId);
        }
    }
}
