using ChronoTrack.Model.DTOs.TimeEntry;
using ChronoTrack.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        public Task<TimeEntryDto?> CreateTimeEntryAsync(string userId, CreateTimeEntryDto createTimeEntryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTimeEntryAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryDto>> GetProjectTimeEntriesAsync(int projectId, string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<TimeEntryDto?> GetTimeEntryByIdAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeEntryDto>> GetUserTimeEntriesAsync(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<TimeEntryDto?> UpdateTimeEntryAsync(int id, string userId, UpdateTimeEntryDto updateTimeEntryDto)
        {
            throw new NotImplementedException();
        }
    }
}
