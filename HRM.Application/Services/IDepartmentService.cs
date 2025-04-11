using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;

namespace HRM.Application.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentResponseDTO> GetDepartmentByIdAsync(Guid id);
        Task<IEnumerable<DepartmentResponseDTO>> GetAllDepartmentsAsync();
        Task<DepartmentResponseDTO> AddDepartmentAsync(DepartmentRequestDTO departmentRequest);
        Task<bool> UpdateDepartmentAsync(Guid id, DepartmentRequestDTO departmentRequest);
        Task<bool> DeleteDepartmentAsync(Guid id);
    }
}
