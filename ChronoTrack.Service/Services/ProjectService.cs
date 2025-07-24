using ChronoTrack.Model.DTOs.Project;
using ChronoTrack.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Services
{
    public class ProjectService : IProjectService
    {
        public Task<bool> AssignUserToProjectAsync(int projectId, string userId, string role = "Member")
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto?> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectDto>> GetOrganizationProjectsAsync(int organizationId)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectDto>> GetUserProjectsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromProjectAsync(int projectId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
