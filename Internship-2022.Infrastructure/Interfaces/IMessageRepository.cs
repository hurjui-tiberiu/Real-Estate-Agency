using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Infrastructure.Interfaces
{
    public interface IMessageRepository
    {
        Task SendMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync(Guid listingId);
        Task DeleteMessageAsync(Guid listingId);
    }
}
