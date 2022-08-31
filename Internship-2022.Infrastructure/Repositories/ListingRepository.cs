using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Contexts;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {
        static List<Listing> list=new List<Listing>();
        private readonly EFContext context;

        public ListingRepository(EFContext context)
        {
            this.context = context;
        }

        public async Task CreateListingAsync(Listing listing)
        {   
            context.Add(listing);
            await context.SaveChangesAsync();
        }

        public async Task DeleteListingAsync(Guid id)
        {
            var listingToDelete = await context.Listings.FirstAsync(entity => entity.Id == id);
            context.Listings.Remove(listingToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<List<Listing>> GetListingsByUserIdAsync(Guid userId)
        {
            var listings = await context.Listings.Where(entity => entity.UserId == userId).ToListAsync();
            return listings;
        }

        public async Task<Listing> GetListingByIdAsync(Guid id)
        {
            var listing = await context.Listings.Where(listing => listing.Id == id).ToListAsync();
           
            return listing.First();
        }

        public async Task<List<Listing>> GetListingAsync()
        {
           return await context.Listings.ToListAsync();
        }

        public async Task UpdateListingAsync(Listing listing)
        {
            context.Update(listing);
            await context.SaveChangesAsync();
        }
    }
}
