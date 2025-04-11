using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;

namespace HRM.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDTO> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<IEnumerable<EmployeeWithDepartmentDTO>> GetAllEmployeesWithDepartmentNameAsync();
        Task<IEnumerable<EmployeeWithProjectsDTO>> GetAllEmployeesWithProjectsAsync();
        Task<IEnumerable<EmployeeWithSalaryDTO>> FilterEmployeesAsync(EmployeeFilterDTO filter);
        Task<EmployeeResponseDTO> AddEmployeeAsync(EmployeeRequestDTO employeeRequest);
        Task<bool> UpdateEmployeeAsync(Guid id, EmployeeRequestDTO employeeRequest);
        Task<bool> DeleteEmployeeAsync(Guid id);

    }
}
