using AutoMapper;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using System.Drawing;
using System.Text;

namespace Internship_2022.Application.Services
{
    public class ListingService : IListingService

    {
        private readonly IAzureStorageService azureStorageService;
        private readonly IListingRepository listingRepo;
        private readonly IMapper mapper;
        private readonly IFavoriteRepository favoriteRepository;

        public ListingService(IListingRepository listingRepo, IConfiguration configuration, IMapper mapper, 
            IAzureStorageService azureStorageService, IFavoriteRepository favoriteRepository) 
        { 
            this.listingRepo = listingRepo;
            this.mapper = mapper;
            this.azureStorageService = azureStorageService;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<List<GetListingRequestDto>> GetListingsByUserIdAsync(Guid userId)
        {
            var listings = await listingRepo.GetListingsByUserIdAsync(userId);
            return mapper.Map<List<GetListingRequestDto>>(listings);

        }

        public async Task CreateListingAsync(CreateListingRequestDto listingDto, Guid userId)
        {
            var listing = mapper.Map<Listing>(listingDto);
            listing.UserId = userId;
            listing.ShortDescription = "ShortDescription";
            listing.Status = true;
            listing.ViewCounter = 0;
            listing.CreatedUtc = DateTime.UtcNow;
            listing.UpdatedUtc = DateTime.UtcNow;
            listing.Images = await azureStorageService.UploadStreamListing(listingDto.Images);

            await listingRepo.CreateListingAsync(listing);
        }

        public async Task DeleteListingAsync(Guid id)
        {
            await favoriteRepository.DeleteAllFavoritesByListingId(id);
            await listingRepo.DeleteListingAsync(id);
        }

        public async Task<GetListingByIdRequestDto> GetListingByIdAsync(Guid id)
        {
            var listing = await listingRepo.GetListingByIdAsync(id);

            return mapper.Map<GetListingByIdRequestDto>(listing);
        }

        public async Task UpdateListingAsync(Guid id, UpdateListingRequestDto listingDto)
        {
            var listing = await listingRepo.GetListingByIdAsync(id);
            var mapperListing = mapper.Map<UpdateListingRequestDto, Listing>(listingDto, listing);
            mapperListing.Images= await azureStorageService.UploadStreamListing(listingDto.Images);

            await listingRepo.UpdateListingAsync(mapperListing);
        }

        public async Task<List<GetListingRequestDto>> GetListingAsync()
        {
            var listing = await listingRepo.GetListingAsync();

            return mapper.Map<List<GetListingRequestDto>>(listing);
        }
    }
}
