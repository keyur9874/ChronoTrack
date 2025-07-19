using ChronoTrack.Model.DTOs.TimeEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface ITimeEntryService
    {
        Task<TimeEntryDto?> CreateTimeEntryAsync(string userId, CreateTimeEntryDto createTimeEntryDto);
        Task<TimeEntryDto?> UpdateTimeEntryAsync(int id, string userId, UpdateTimeEntryDto updateTimeEntryDto);
        Task<bool> DeleteTimeEntryAsync(int id, string userId);
        Task<TimeEntryDto?> GetTimeEntryByIdAsync(int id, string userId);
        Task<List<TimeEntryDto>> GetUserTimeEntriesAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<TimeEntryDto>> GetProjectTimeEntriesAsync(int projectId, string userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}