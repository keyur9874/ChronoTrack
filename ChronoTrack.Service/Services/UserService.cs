using ChronoTrack.Model.DTOs.Auth;
using ChronoTrack.Repository.Interfaces;
using ChronoTrack.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Services
{
    public class UserService(IUserRepository userRepository): IUserService
    {
        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            return user != null ? new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                LoginType = user.LoginType,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt,
            } : null;
        }
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetByEmailAsync(email);
            return user != null ? new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                LoginType = user.LoginType,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt,
            } : null;
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            try
            {
                await userRepository.DeleteAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
