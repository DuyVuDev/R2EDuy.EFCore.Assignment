using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Application.Services;
using HRM.Domain.Entities;

namespace HRM.Infrastructure.ServiceImplements
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => new ProjectResponseDTO
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<ProjectResponseDTO> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }
            return new ProjectResponseDTO
            {
                Id = project.Id,
                Name = project.Name
            };
        }

        public async Task<ProjectResponseDTO> AddProjectAsync(ProjectRequestDTO projectRequest)
        {
            var project = new Project
            {
                Name = projectRequest.Name,
            };

            await _projectRepository.AddAsync(project);

            await _projectRepository.SaveChangesAsync();

            return new ProjectResponseDTO
            {
                Id = project.Id,
                Name = project.Name
            };
        }

        public async Task<bool> UpdateProjectAsync(Guid id, ProjectRequestDTO projectRequest)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            existingProject.Name = projectRequest.Name;

            await _projectRepository.UpdateAsync(existingProject);
            await _projectRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            await _projectRepository.DeleteAsync(existingProject);
            await _projectRepository.SaveChangesAsync();

            return true;
        }
    }
}
