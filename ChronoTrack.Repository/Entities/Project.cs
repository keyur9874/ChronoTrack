using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Foreign keys
        public int OrganizationId { get; set; }

        // Navigation properties
        public Organization Organization { get; set; } = null!;
        public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}