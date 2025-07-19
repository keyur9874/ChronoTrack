using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Model.DTOs.Project
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? HourlyRate { get; set; }

        [Required]
        public int OrganizationId { get; set; }
    }
}