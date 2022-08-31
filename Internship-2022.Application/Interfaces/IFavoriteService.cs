using Internship_2022.Application.Models.FavoriteDto;
using Internship_2022.Application.Models.ListingDtos;

namespace Internship_2022.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddFavorite(Guid userId, Guid listingId);
        Task DeleteFavorite(Guid userId, Guid listingId);
        Task<List<FavoriteRequestDto>> GetFavoritesByUserId(Guid userId);
        Task<List<FavoriteRequestDto>> GetFavoritesByListingId(Guid listingId);
        Task<List<FavoriteRequestDto>> GetFavoriteByFavoriteId(Guid favoriteId);
        Task DeleteById(Guid id);
        Task<List<GetListingRequestDto>> GetListingFavoriteByUserId(Guid userId);
    }
}
