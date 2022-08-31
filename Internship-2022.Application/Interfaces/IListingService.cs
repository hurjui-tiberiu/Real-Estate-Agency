using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Domain.Entities;

namespace Internship_2022.Application.Interfaces
{
    public interface IListingService
    {
        Task CreateListingAsync(CreateListingRequestDto listing,Guid userId);
        Task<List<GetListingRequestDto>> GetListingAsync();
        Task UpdateListingAsync(Guid id, UpdateListingRequestDto listing);
        Task<GetListingByIdRequestDto> GetListingByIdAsync(Guid id);
        Task DeleteListingAsync(Guid id);
        Task<List<GetListingRequestDto>> GetListingsByUserIdAsync(Guid userId);
    }
}
