namespace Internship_2022.Application.Models.MessageDto
{
    public class MessageRequestDto
    {
        public Guid SenderId { get; init; }
        public Guid ReceiverId { get; init; }
        public string? Content { get; init; }
        public Guid ListingId { get; set; }
    }
}
