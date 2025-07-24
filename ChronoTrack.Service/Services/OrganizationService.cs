using ChronoTrack.Model.DTOs.Organization;
using ChronoTrack.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Services
{
    public class OrganizationService : IOrganizationService
    {
        public Task<bool> AddUserToOrganizationAsync(int organizationId, string userId, string role = "Member")
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationDto?> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationDto?> GetOrganizationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrganizationDto>> GetUserOrganizationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromOrganizationAsync(int organizationId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
