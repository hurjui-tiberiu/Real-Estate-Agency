using System.Collections.ObjectModel;

namespace Internship_2022.Domain.Entities
{
    public record UserActivity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Device { get; set; }
        public string? DeviceType { get; set; }
        public string? Location { get; set; }
        public DateTime ConnectionDate { get; set; }
        public bool Status { get; set; }

        public User? User { get; set; }
    }
}
