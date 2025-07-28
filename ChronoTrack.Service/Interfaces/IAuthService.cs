using ChronoTrack.Model.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> HandleExternalLoginAsync(string email, string firstName, string lastName, LoginType loginType);
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(Guid userId);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}