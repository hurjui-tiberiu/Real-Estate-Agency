using Internship_2022.Domain.Entities;

namespace Internship_2022.Infrastructure.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddFavorite(Guid userId, Guid listingId);
        Task DeleteFavorite(Guid userId, Guid listingId);
        Task<List<Favorite>> GetFavorites(Guid userId);
        Task<Favorite> GetFavoriteById(Guid id);
        Task<Favorite> GetFavoriteByFavoriteId(Guid id);
        Task DeleteById(Favorite favoriteid);
        Task<List<Listing>> GetAllListingsByUserId(Guid userId);
        Task DeleteAllFavoritesByListingId(Guid listingId);
    }
}
