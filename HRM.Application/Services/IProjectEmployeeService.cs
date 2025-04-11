using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;

namespace HRM.Application.Services
{
    public interface IProjectEmployeeService
    {
        Task<IEnumerable<ProjectEmployeeResponseDTO>> GetAllProjectEmployeesAsync();
        Task<ProjectEmployeeResponseDTO> GetProjectEmployeeByIdsAsync(Guid ProjectId, Guid EmployeeId);
        Task<ProjectEmployeeResponseDTO> AddProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId);
        Task<bool> UpdateProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId, ProjectEmployeeRequestDTO projectEmployeeRequestDTO);
        Task<bool> DeleteProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId);
    }
}
