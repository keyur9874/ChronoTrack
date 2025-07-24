using ChronoTrack.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken();
        Task SaveRefreshTokenAsync(Guid userId, RefreshToken token);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task ReplaceRefreshTokenAsync(Guid userId, string oldToken, RefreshToken newToken);
        Task RevokeAllTokensForUserAsync(Guid userId);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
