namespace Internship_2022.Domain.Entities
{
    public record Favorite
    {
        public Guid Id { get; set; }
        public Guid UserId { get; init; }
        public Guid ListingId { get; init; }
        public User? User { get; set; }
        public Listing? Listing { get; set; }
    }
}
