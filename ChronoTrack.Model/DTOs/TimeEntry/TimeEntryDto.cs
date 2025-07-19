using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Model.DTOs.TimeEntry
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal HoursWorked { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}