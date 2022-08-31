using Internship_2022.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_2022.Domain.Configurations
{
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder
                .HasOne(entity => entity.User)
                .WithMany(entity => entity.UserActivities)
                .HasForeignKey(entity => entity.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Device)
                .IsRequired();

            builder.Property(x => x.DeviceType)
               .IsRequired();

            builder.Property(x => x.Location)
               .IsRequired();

            builder.Property(x => x.ConnectionDate)
               .IsRequired();

            builder.Property(x => x.Status)
               .IsRequired();
        }
    }
}
