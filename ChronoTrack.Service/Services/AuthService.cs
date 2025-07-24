using ChronoTrack.Model.DTOs.Auth;
using ChronoTrack.Repository.Entities;
using ChronoTrack.Repository.Interfaces;
using ChronoTrack.Service.Interfaces;

namespace ChronoTrack.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null) return null;

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(registerDto.Password)
            };

            await _userRepository.CreateAsync(user);

            var token = _jwtTokenService.GenerateToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();
            await _refreshTokenService.SaveRefreshTokenAsync(user.Id, refreshToken);

            return new AuthResponseDto
            {
                Token = _jwtTokenService.WriteToken(token),
                RefreshToken = refreshToken.Token,
                ExpiryDate = token.ValidTo,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                }
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null) return null;

            if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            var token = _jwtTokenService.GenerateToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();
            await _refreshTokenService.SaveRefreshTokenAsync(user.Id, refreshToken);

            return new AuthResponseDto
            {
                Token = _jwtTokenService.WriteToken(token),
                RefreshToken = refreshToken.Token,
                ExpiryDate = token.ValidTo,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                }
            };
        }        

        public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenService.GetRefreshTokenAsync(refreshToken);
            if (storedToken == null || storedToken.IsExpired || storedToken.IsRevoked)
                return null;

            var user = await _userRepository.GetByIdAsync(storedToken.UserId);
            if (user == null)
                return null;

            var newJwtToken = _jwtTokenService.GenerateToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();
            await _refreshTokenService.ReplaceRefreshTokenAsync(user.Id, refreshToken, newRefreshToken);

            return new AuthResponseDto
            {
                Token = _jwtTokenService.WriteToken(newJwtToken),
                RefreshToken = newRefreshToken.Token,
                ExpiryDate = newJwtToken.ValidTo,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                }
            };
        }

        public async Task<bool> LogoutAsync(Guid userId)
        {
            // Remove all refresh tokens for the user (or current session if you track sessions)
            await _refreshTokenService.RevokeAllTokensForUserAsync(userId);
            return true;
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var result = await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);
            return result;
        }
    }
}
