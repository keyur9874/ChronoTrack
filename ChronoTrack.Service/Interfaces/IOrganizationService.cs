using ChronoTrack.Model.DTOs.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto?> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto, string userId);
        Task<OrganizationDto?> GetOrganizationByIdAsync(int id);
        Task<List<OrganizationDto>> GetUserOrganizationsAsync(string userId);
        Task<bool> AddUserToOrganizationAsync(int organizationId, string userId, string role = "Member");
        Task<bool> RemoveUserFromOrganizationAsync(int organizationId, string userId);
    }
}