using ChronoTrack.Model.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTrack.Service.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto?> CreateProjectAsync(CreateProjectDto createProjectDto);
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<List<ProjectDto>> GetUserProjectsAsync(string userId);
        Task<List<ProjectDto>> GetOrganizationProjectsAsync(int organizationId);
        Task<bool> AssignUserToProjectAsync(int projectId, string userId, string role = "Member");
        Task<bool> RemoveUserFromProjectAsync(int projectId, string userId);
    }
}