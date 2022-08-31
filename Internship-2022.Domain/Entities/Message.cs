namespace Internship_2022.Domain.Entities
{
    public record Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid ListingId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Content { get; set; }
        public bool ViewStatus { get; set; }
        public User? Receiver { get; set; }
        public User? Sender { get; set; }
        public Listing? Listing { get; set; }
    }
}
