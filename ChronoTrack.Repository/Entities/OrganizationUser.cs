using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class OrganizationUser
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public int OrganizationId { get; set; }
        public string Role { get; set; } = "Member"; // Admin, Member
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
    }
}