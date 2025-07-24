using ChronoTrack.Repository.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ChronoTrack.Service.Interfaces
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GenerateToken(User user);
        string WriteToken(JwtSecurityToken token);
    }
}
