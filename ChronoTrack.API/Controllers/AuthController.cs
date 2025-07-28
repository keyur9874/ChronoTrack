using ChronoTrack.Model.Common;
using ChronoTrack.Model.DTOs.Auth;
using ChronoTrack.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                if (result == null)
                {
                    return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse("Registration failed"));
                }

                return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "User registered successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                if (result == null)
                {
                    return Unauthorized(ApiResponse<AuthResponseDto>.ErrorResponse("Invalid credentials"));
                }

                return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("external-login")]
        public IActionResult ExternalLogin()
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Auth");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl ?? "/" };
            return Challenge(properties, "AzureAD");
        }

        [HttpGet("token-received")]
        public IActionResult TokenReceived([FromQuery] string token, [FromQuery] string refresh)
        {
            return Ok(new
            {
                Message = "External login succeeded. Tokens received:",
                AccessToken = token,
                RefreshToken = refresh
            });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(refreshToken);
                if (result == null)
                {
                    return Unauthorized(ApiResponse<AuthResponseDto>.ErrorResponse("Invalid refresh token"));
                }

                return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Token refreshed successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse<bool>>> Logout([FromBody] Guid userId)
        {
            try
            {
                var result = await _authService.LogoutAsync(userId);
                return Ok(ApiResponse<bool>.SuccessResponse(result, "Logout successful"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }
}