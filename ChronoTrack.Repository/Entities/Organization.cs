using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
    }
}