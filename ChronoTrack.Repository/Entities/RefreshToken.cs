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
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public Guid UserId { get; set; } = Guid.Empty;

        // Navigation property
        public User User { get; set; } = null!;

        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}