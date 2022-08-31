using Internship_2022.Application.Models.MessageDto;
using Internship_2022.Domain.Entities;

namespace Internship_2022.Application.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(MessageRequestDto message);
        Task<List<MessageRequestDto>> GetMessagesAsync(Guid listingId);
        Task DeleteMessageAsync(Guid listingId);
    }
}
