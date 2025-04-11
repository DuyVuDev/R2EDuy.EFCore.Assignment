using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Domain.Entities;

namespace HRM.Application.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<EmployeeWithDepartmentDTO>> GetAllWithDepartmentAsync();
        Task<IEnumerable<EmployeeWithProjectsDTO>> GetAllWithProjectsAsync();
        Task<IEnumerable<EmployeeWithSalaryDTO>> FilterEmployeeAsync(EmployeeFilterDTO employeeFilterDTO);
    }
}
