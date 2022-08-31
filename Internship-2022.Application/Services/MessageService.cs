using AutoMapper;
using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.MessageDto;
using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Interfaces;

namespace Internship_2022.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepo;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepo, IMapper mapper)
        {
            this.messageRepo = messageRepo;
            this.mapper = mapper;
        }

        public async Task DeleteMessageAsync(Guid listingId)
        {
            await messageRepo.DeleteMessageAsync(listingId);
        }

        public async Task<List<MessageRequestDto>> GetMessagesAsync(Guid listingId)
        {
            var messages = await messageRepo.GetMessagesAsync(listingId);

            return mapper.Map<List<MessageRequestDto>>(messages);
        }

        public async Task SendMessageAsync(MessageRequestDto messageDto)
        {
            var message = mapper.Map<Message>(messageDto);
            message.CreatedAt = DateTime.Now;
            message.UpdatedAt = DateTime.Now;
            message.ViewStatus = false;

            await messageRepo.SendMessageAsync(message);
        }
    }
}
