using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Model.DTOs.TimeEntry
{
    public class UpdateTimeEntryDto
    {
        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        [Range(0.01, 24.0)]
        public decimal HoursWorked { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}