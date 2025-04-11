using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;

namespace HRM.Application.Services
{
    public interface IProjectService
    {
        Task<ProjectResponseDTO> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync();
        Task<ProjectResponseDTO> AddProjectAsync(ProjectRequestDTO projectRequest);
        Task<bool> UpdateProjectAsync(Guid id, ProjectRequestDTO projectRequest);
        Task<bool> DeleteProjectAsync(Guid id);
    }
}
