using HRM.Application.DTOs.Requests;
using HRM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentRequestDTO departmentRequest)
        {
            try
            {
                var department = await _departmentService.AddDepartmentAsync(departmentRequest);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] DepartmentRequestDTO departmentRequest)
        {
            try
            {
                var result = await _departmentService.UpdateDepartmentAsync(id, departmentRequest);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartmentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
