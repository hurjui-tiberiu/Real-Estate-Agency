using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Contexts;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Internship_2022.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly EFContext context;
        private IListingRepository listingRepo;

        public FavoriteRepository(EFContext context,IListingRepository listingRepository)
        {
            this.context = context;
            this.listingRepo = listingRepository;
        }

        public async Task<List<Favorite>> GetFavorites(Guid userId)
        {
            return await context.Favorites.Where(entity => entity.UserId == userId).ToListAsync();
        }

        public async Task AddFavorite(Guid userId, Guid listingId)
        {

            context.Add(new Favorite { UserId = userId, ListingId = listingId });
            await context.SaveChangesAsync();
        }

        public async Task DeleteFavorite(Guid userId, Guid listingId)
        {
            var favoriteToDelete = await context.Favorites
                .Where(entity => entity.UserId == userId && entity.ListingId == listingId)
                .ToListAsync();
            context.Favorites.RemoveRange(favoriteToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<Favorite> GetFavoriteById(Guid id)
        {
            var favorite = await context.Favorites.Where(favorite => favorite.UserId == id).ToListAsync();

            return favorite.First();
        }

        public async Task<Favorite> GetFavoriteByFavoriteId(Guid id)
        {
            var favorite = await context.Favorites.Where(favorite => favorite.Id == id).ToListAsync();

            return favorite.First();
        }

        public async Task DeleteAllFavoritesByListingId(Guid listingId)
        {
             context.Favorites.RemoveRange(context.Favorites.Where(entity => entity.ListingId == listingId));

            await context.SaveChangesAsync();
        }

        public async Task DeleteById(Favorite favoriteid)
        {
            context.Favorites.Remove(favoriteid);
            await context.SaveChangesAsync();
        }

        public async Task<List<Listing>> GetAllListingsByUserId(Guid userId)
        {
            var favorites = await context.Favorites
                .Where(entity => entity.UserId == userId).ToListAsync();

            List<Listing> listing = new List<Listing>();

            foreach (var variable in favorites)
            {
                listing.Add(await listingRepo.GetListingByIdAsync(variable.ListingId));
            }

            return listing;
        }
        
    }
}
