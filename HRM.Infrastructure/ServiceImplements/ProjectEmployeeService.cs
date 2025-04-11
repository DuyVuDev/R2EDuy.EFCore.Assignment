using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Application.Services;
using HRM.Domain.Entities;

namespace HRM.Infrastructure.ServiceImplements
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;


        public ProjectEmployeeService(IProjectEmployeeRepository projectEmployeeRepository)
        {
            _projectEmployeeRepository = projectEmployeeRepository;

        }

        public async Task<IEnumerable<ProjectEmployeeResponseDTO>> GetAllProjectEmployeesAsync()
        {
            var projectEmployees = await _projectEmployeeRepository.GetAllAsync();
            return projectEmployees.Select(pe => new ProjectEmployeeResponseDTO
            {
                ProjectId = pe.ProjectId,
                EmployeeId = pe.EmployeeId,
                Enabled = pe.Enabled,
            });
        }

        public async Task<ProjectEmployeeResponseDTO> GetProjectEmployeeByIdsAsync(Guid ProjectId, Guid EmployeeId)
        {
            var projectEmployee = await _projectEmployeeRepository.GetByKeysAsync(ProjectId, EmployeeId);
            if (projectEmployee == null)
            {
                throw new KeyNotFoundException($"ProjectEmployee with ProjectId {ProjectId} and EmployeeId {EmployeeId} not found.");
            }
            return new ProjectEmployeeResponseDTO
            {
                ProjectId = projectEmployee.ProjectId,
                EmployeeId = projectEmployee.EmployeeId,
                Enabled = projectEmployee.Enabled,
            };
        }

        public async Task<ProjectEmployeeResponseDTO> AddProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId)
        {
            var existingProjectEmployee = await _projectEmployeeRepository.GetByKeysAsync(ProjectId, EmployeeId);
            if (existingProjectEmployee != null)
            {
                throw new Exception("ProjectEmployee already exists");
            }
            var newProjectEmployee = new ProjectEmployee
            {
                ProjectId = ProjectId,
                EmployeeId = EmployeeId,
                Enabled = true,
            };
            await _projectEmployeeRepository.AddAsync(newProjectEmployee);
            await _projectEmployeeRepository.SaveChangesAsync();
            return new ProjectEmployeeResponseDTO
            {
                ProjectId = newProjectEmployee.ProjectId,
                EmployeeId = newProjectEmployee.EmployeeId,
                Enabled = newProjectEmployee.Enabled,
            };
        }

        public async Task<bool> UpdateProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId, ProjectEmployeeRequestDTO projectEmployeeRequestDTO)
        {
            var existingProjectEmployee = await _projectEmployeeRepository.GetByKeysAsync(ProjectId, EmployeeId);
            if (existingProjectEmployee == null)
            {
                throw new Exception("ProjectEmployee not found");
            }
            existingProjectEmployee.Enabled = projectEmployeeRequestDTO.Enabled;
            await _projectEmployeeRepository.UpdateAsync(existingProjectEmployee);
            await _projectEmployeeRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectEmployeeAsync(Guid ProjectId, Guid EmployeeId)
        {
            var existingProjectEmployee = await _projectEmployeeRepository.GetByKeysAsync(ProjectId, EmployeeId);
            if (existingProjectEmployee == null)
            {
                throw new Exception("ProjectEmployee not found");
            }
            await _projectEmployeeRepository.DeleteAsync(existingProjectEmployee);
            await _projectEmployeeRepository.SaveChangesAsync();
            return true;
        }
    }
}
