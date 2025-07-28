using ChronoTrack.Model.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
