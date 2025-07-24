using ChronoTrack.Repository.Data;
using ChronoTrack.Repository.Entities;
using ChronoTrack.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Service.Services
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly ChronoTrackDbContext _db;

        public RefreshTokenService(ChronoTrackDbContext db)
        {
            _db = db;
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task SaveRefreshTokenAsync(Guid userId, RefreshToken token)
        {
            token.UserId = userId;
            _db.RefreshTokens.Add(token);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked && rt.Expires > DateTime.UtcNow);
        }

        public async Task ReplaceRefreshTokenAsync(Guid userId, string oldToken, RefreshToken newToken)
        {
            var old = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == oldToken && rt.UserId == userId);
            if (old != null)
            {
                old.IsRevoked = true;
                _db.RefreshTokens.Update(old);
            }
            newToken.UserId = userId;
            _db.RefreshTokens.Add(newToken);
            await _db.SaveChangesAsync();
        }

        public async Task RevokeAllTokensForUserAsync(Guid userId)
        {
            var tokens = await _db.RefreshTokens.Where(rt => rt.UserId == userId && !rt.IsRevoked).ToListAsync();
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
            _db.RefreshTokens.UpdateRange(tokens);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null) return false;
            refreshToken.IsRevoked = true;
            _db.RefreshTokens.Update(refreshToken);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
