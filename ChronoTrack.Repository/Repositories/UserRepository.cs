using ChronoTrack.Repository.Data;
using ChronoTrack.Repository.Entities;
using ChronoTrack.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChronoTrackDbContext _db;

        public UserRepository(ChronoTrackDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}