using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Repository.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public string UserId { get; set; } = string.Empty;

        // Navigation property
        public User User { get; set; } = null!;
    }
}