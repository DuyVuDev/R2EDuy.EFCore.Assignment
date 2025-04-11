using HRM.Application.DTOs.Requests;
using HRM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalariesController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalaries()
        {
            try
            {
                var salaries = await _salaryService.GetAllSalarysAsync();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaryById(Guid id)
        {
            try
            {
                var salary = await _salaryService.GetSalaryByIdAsync(id);
                return Ok(salary);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalary(Guid id, [FromBody] SalaryRequestDTO salaryRequest)
        {
            try
            {
                if (salaryRequest == null)
                {
                    return BadRequest("Salary data is required.");
                }
                var updatedSalary = await _salaryService.UpdateSalaryAsync(id, salaryRequest);

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
