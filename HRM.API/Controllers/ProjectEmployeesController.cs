using HRM.Application.DTOs.Requests;
using HRM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly IProjectEmployeeService _projectEmployeeService;
        public ProjectEmployeesController(IProjectEmployeeService projectEmployeeService)
        {
            _projectEmployeeService = projectEmployeeService;
        }

        [HttpGet("/{projectId}/{employeeId}")]
        public async Task<IActionResult> GetProjectEmployeesByKeys(Guid projectId, Guid employeeId)
        {
            try
            {
                var projectEmployee = await _projectEmployeeService.GetProjectEmployeeByIdsAsync(projectId, employeeId);
                return Ok(projectEmployee);
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

        [HttpGet]
        public async Task<IActionResult> GetAllProjectEmployees()
        {
            try
            {
                var projectEmployees = await _projectEmployeeService.GetAllProjectEmployeesAsync();
                return Ok(projectEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{projectId}/{employeeId}")]
        public async Task<IActionResult> AddProjectEmployee(Guid projectId, Guid employeeId)
        {
            try
            {
                var projectEmployee = await _projectEmployeeService.AddProjectEmployeeAsync(projectId, employeeId);
                return CreatedAtAction(nameof(GetProjectEmployeesByKeys), new { projectId = projectEmployee.ProjectId, employeeId = projectEmployee.EmployeeId }, projectEmployee);
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

        [HttpPut("{projectId}/{employeeId}")]
        public async Task<IActionResult> UpdateProjectEmployee(Guid projectId, Guid employeeId, [FromBody] ProjectEmployeeRequestDTO projectEmployeeRequestDTO)
        {
            try
            {
                var result = await _projectEmployeeService.UpdateProjectEmployeeAsync(projectId, employeeId, projectEmployeeRequestDTO);
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

        [HttpDelete("{projectId}/{employeeId}")]
        public async Task<IActionResult> DeleteProjectEmployee(Guid projectId, Guid employeeId)
        {
            try
            {
                var result = await _projectEmployeeService.DeleteProjectEmployeeAsync(projectId, employeeId);
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