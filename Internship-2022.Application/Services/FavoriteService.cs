using AutoMapper;
using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.FavoriteDto;
using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Infrastructure.Interfaces;

namespace Internship_2022.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepo;
        private readonly IMapper mapper;

        public FavoriteService(IFavoriteRepository favoriteRepo, IMapper mapper)
        {
            this.favoriteRepo = favoriteRepo;
            this.mapper = mapper;
        }

        public async Task<bool> AddFavorite(Guid userId, Guid listingId)
        {
            var favoritesUser = await favoriteRepo.GetFavorites(userId);
            var favorite= favoritesUser.FirstOrDefault(entity=>entity.ListingId==listingId);
            if(favorite!=null)
            {
                return false;
            }

            await favoriteRepo.AddFavorite(userId, listingId);
            return true;
        }

        public async Task DeleteFavorite(Guid userId, Guid listingId)
        {

            await favoriteRepo.DeleteFavorite(userId, listingId);
        }

        public async Task<List<FavoriteRequestDto>> GetFavoritesByUserId(Guid userId)
        {
            var favorites = await favoriteRepo.GetFavorites(userId);
            
            return mapper.Map<List<FavoriteRequestDto>>(favorites);
        }

        public async Task<List<FavoriteRequestDto>> GetFavoritesByListingId(Guid listingId)
        {
            var favorites = await favoriteRepo.GetFavorites(listingId);

            return mapper.Map<List<FavoriteRequestDto>>(favorites);
        }

        public async Task<List<FavoriteRequestDto>> GetFavoriteByFavoriteId(Guid favoriteId)
        {
            var favorites = await favoriteRepo.GetFavoriteByFavoriteId(favoriteId);

            return mapper.Map<List<FavoriteRequestDto>>(favorites);
        }

        public async Task DeleteById(Guid id)
        {
            var favorites = await favoriteRepo.GetFavoriteByFavoriteId(id);

            await favoriteRepo.DeleteById(favorites);
        }

        public async Task<List<GetListingRequestDto>> GetListingFavoriteByUserId(Guid userId)
        {
            var favorites = await favoriteRepo.GetAllListingsByUserId(userId);

            return mapper.Map<List<GetListingRequestDto>>(favorites);
        }

    }
}
