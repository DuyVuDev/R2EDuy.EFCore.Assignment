using HRM.Application.DTOs.Requests;
using HRM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Departments")]
        public async Task<IActionResult> GetAllEmployeesWithDepartmentNames()
        {
            try
            {
                var employeesWithDepartments = await _employeeService.GetAllEmployeesWithDepartmentNameAsync();
                return Ok(employeesWithDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetAllEmployeesWithProjectNames()
        {
            try
            {
                var employeesWithProjects = await _employeeService.GetAllEmployeesWithProjectsAsync();
                return Ok(employeesWithProjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterEmployees([FromQuery] EmployeeFilterDTO filter)
        {
            try
            {
                var result = await _employeeService.FilterEmployeesAsync(filter);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRequestDTO employeeRequest)
        {
            try
            {
                if (employeeRequest == null)
                {
                    return BadRequest("Employee data is required.");
                }
                var employee = await _employeeService.AddEmployeeAsync(employeeRequest);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeRequestDTO employeeRequest)
        {
            try
            {
                if (employeeRequest == null)
                {
                    return BadRequest("Employee data is required.");
                }
                var result = await _employeeService.UpdateEmployeeAsync(id, employeeRequest);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}