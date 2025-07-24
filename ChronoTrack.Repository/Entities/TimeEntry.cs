using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal HoursWorked { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        public Guid UserId { get; set; } = Guid.Empty;
        public int ProjectId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}