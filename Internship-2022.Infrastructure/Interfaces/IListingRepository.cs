using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Infrastructure.Interfaces
{
    public interface IListingRepository
    {
        Task CreateListingAsync(Listing listing);
        Task<List<Listing>> GetListingAsync();
        Task UpdateListingAsync(Listing listing);
        Task<Listing> GetListingByIdAsync(Guid id);
        Task DeleteListingAsync(Guid id);
        Task<List<Listing>> GetListingsByUserIdAsync(Guid userId);
    }
}
