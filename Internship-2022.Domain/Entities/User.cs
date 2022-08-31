using Internship_2022.Domain.Enum;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Internship_2022.Domain.Entities
{
    public record User
    {
        public User()
        {
            UserActivities = new Collection<UserActivity>();
            Listings = new Collection<Listing>();
            Favorites = new Collection<Favorite>();
        }

        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public EGender? Gender { get; set; }
        public string? Phone { get; set; }
        public ERole Role { get; set; }
        public string? NotificationPreferences { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserActivity>? UserActivities { get; set; }
        public ICollection<Listing>? Listings { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public Message? MessageReceiver { get; set; }
        public Message? MessageSender { get; set; }
    }
}
