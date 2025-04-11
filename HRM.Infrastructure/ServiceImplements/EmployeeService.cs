using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Application.Services;
using HRM.Domain.Entities;

namespace HRM.Infrastructure.ServiceImplements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IProjectRepository _projectRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            ISalaryRepository salaryRepository,
            IProjectEmployeeRepository projectEmployeeRepository,
            IProjectRepository projectRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _salaryRepository = salaryRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectRepository = projectRepository;
        }



        public async Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => new EmployeeResponseDTO
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
                JoinedDate = e.JoinedDate
            });
        }

        public async Task<IEnumerable<EmployeeWithDepartmentDTO>> GetAllEmployeesWithDepartmentNameAsync()
        {
            return await _employeeRepository.GetAllWithDepartmentAsync();
        }

        public async Task<IEnumerable<EmployeeWithProjectsDTO>> GetAllEmployeesWithProjectsAsync()
        {
            return await _employeeRepository.GetAllWithProjectsAsync();
        }

        public async Task<EmployeeResponseDTO> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return new EmployeeResponseDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                DepartmentId = employee.DepartmentId,
                JoinedDate = employee.JoinedDate
            };
        }


        public async Task<EmployeeResponseDTO> AddEmployeeAsync(EmployeeRequestDTO employeeRequest)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(employeeRequest.DepartmentId);
            if (existingDepartment == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            var employee = new Employee
            {
                Name = employeeRequest.Name,
                DepartmentId = employeeRequest.DepartmentId,
                JoinedDate = employeeRequest.JoinedDate,
                ProjectEmployees = new List<ProjectEmployee>()
            };

            employee.Salary = new Salary
            {
                EmployeeId = employee.Id,
                Employee = employee,
                Amount = employeeRequest.Amount
            };

            if (employeeRequest.ProjectIds != null && employeeRequest.ProjectIds.Any())
            {
                foreach (var projectId in employeeRequest.ProjectIds)
                {
                    var project = await _projectRepository.GetByIdAsync(projectId);
                    if (project == null)
                    {
                        throw new KeyNotFoundException($"Project with ID {projectId} not found.");
                    }
                    var projectEmployee = new ProjectEmployee
                    {
                        Employee = employee,
                        EmployeeId = employee.Id,
                        Project = project,
                        ProjectId = projectId,
                        Enabled = true
                    };
                    employee.ProjectEmployees.Add(projectEmployee);
                }
            }

            await _employeeRepository.AddAsync(employee);

            await _employeeRepository.SaveChangesAsync();

            return new EmployeeResponseDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                DepartmentId = employee.DepartmentId,
                JoinedDate = employee.JoinedDate
            };
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, EmployeeRequestDTO employeeRequest)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            var existingDepartment = await _departmentRepository.GetByIdAsync(employeeRequest.DepartmentId);
            if (existingDepartment == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            existingEmployee.Name = employeeRequest.Name;
            existingEmployee.DepartmentId = employeeRequest.DepartmentId;
            existingEmployee.JoinedDate = employeeRequest.JoinedDate;

            var currentProjects = await _projectEmployeeRepository.GetByEmployeeIdAsync(id);
            if (currentProjects != null)
            {
                await _projectEmployeeRepository.DeleteRangeAsync(currentProjects);
            }

            var newProjects = employeeRequest.ProjectIds?.Select(projectId => new ProjectEmployee
            {
                EmployeeId = id,
                ProjectId = projectId,
                Enabled = true
            }).ToList() ?? new List<ProjectEmployee>();

            await _projectEmployeeRepository.AddRangeAsync(newProjects);

            var existingSalary = await _salaryRepository.GetByEmployeeIdAsync(id);
            if (existingSalary != null)
            {
                existingSalary.Amount = employeeRequest.Amount;
                await _salaryRepository.UpdateAsync(existingSalary);
            }
            else
            {
                existingSalary = new Salary
                {
                    EmployeeId = id,
                    Amount = employeeRequest.Amount
                };
                await _salaryRepository.AddAsync(existingSalary);
            }

            await _employeeRepository.UpdateAsync(existingEmployee);
            await _employeeRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            var currentProjects = await _projectEmployeeRepository.GetByEmployeeIdAsync(id);
            if (currentProjects != null && currentProjects.Any())
            {
                await _projectEmployeeRepository.DeleteRangeAsync(currentProjects);
            }

            var existingSalary = await _salaryRepository.GetByEmployeeIdAsync(id);
            if (existingSalary != null)
            {
                await _salaryRepository.DeleteAsync(existingSalary);
            }
            await _employeeRepository.DeleteAsync(existingEmployee);
            await _employeeRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeWithSalaryDTO>> FilterEmployeesAsync(EmployeeFilterDTO filter)
        {
            return await _employeeRepository.FilterEmployeeAsync(filter);
        }
    }
}
