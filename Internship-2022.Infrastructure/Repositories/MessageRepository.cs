using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Contexts;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Internship_2022.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly EFContext context;

        public MessageRepository(EFContext context)
        {
            this.context = context;
        }

        public async Task DeleteMessageAsync(Guid listingId)
        {
            var userToRemove = await context.Messages.FirstAsync(
                entity => entity.ListingId==listingId);
            context.Messages.Remove(userToRemove);
            await context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(Guid listingId)
        {
            return await context.Messages.Where(entity=>entity.ListingId==listingId).ToListAsync();
        }

        public async Task SendMessageAsync(Message message)
        {
            context.Add(message);
            await context.SaveChangesAsync();
        }
    }
}
