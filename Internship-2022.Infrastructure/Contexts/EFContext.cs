using Internship_2022.Domain.Configurations;
using Internship_2022.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Internship_2022.Infrastructure.Contexts
{
    public class EFContext : DbContext
    {
       

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<Favorite> Favorites { get; set; } 
        public DbSet<Listing>Listings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
              modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserActivityConfiguration).Assembly);
              modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfiguration).Assembly);
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListingConfiguration).Assembly);
              modelBuilder.ApplyConfigurationsFromAssembly(typeof(FavoriteConfiguration).Assembly);
        } 
    }
}
