using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class ProjectUser
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public int ProjectId { get; set; }
        public string Role { get; set; } = "Member"; // Manager, Member
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}