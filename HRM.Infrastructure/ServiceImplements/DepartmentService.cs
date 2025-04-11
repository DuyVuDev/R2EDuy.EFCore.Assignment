using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Application.Services;
using HRM.Domain.Entities;

namespace HRM.Infrastructure.ServiceImplements
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentResponseDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return departments.Select(d => new DepartmentResponseDTO
            {
                Id = d.Id,
                Name = d.Name
            });
        }

        public async Task<DepartmentResponseDTO> GetDepartmentByIdAsync(Guid id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }
            return new DepartmentResponseDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<DepartmentResponseDTO> AddDepartmentAsync(DepartmentRequestDTO departmentRequest)
        {
            var department = new Department
            {
                Name = departmentRequest.Name
            };

            await _departmentRepository.AddAsync(department);

            await _departmentRepository.SaveChangesAsync();

            return new DepartmentResponseDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<bool> UpdateDepartmentAsync(Guid id, DepartmentRequestDTO departmentRequest)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);

            if (existingDepartment == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            existingDepartment.Name = departmentRequest.Name;

            await _departmentRepository.UpdateAsync(existingDepartment);

            await _departmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(Guid id)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            await _departmentRepository.DeleteAsync(existingDepartment);

            await _departmentRepository.SaveChangesAsync();

            return true;
        }
    }
}
