using Internship_2022.Domain.Enum;
using System.Collections.ObjectModel;

namespace Internship_2022.Domain.Entities
{
    public record Listing
    {
        public Listing()
        {
            ViewCounter = 0;
            Favorites = new Collection<Favorite>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public double Price { get; set; }
        public Guid Author { get; set; }
        public Guid ApprovedBy { get; set; }
        public bool Status { get; set; }
        public string? Images { get; set; }
        public ECategory Category { get; set; }
        public int ViewCounter { get; set; }
        public DateTime CreatedUtc;
        public DateTime UpdatedUtc;

        public User? User { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public Message? Message { get; set; }
    }
}
