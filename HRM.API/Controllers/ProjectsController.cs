using HRM.Application.DTOs.Requests;
using HRM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                return Ok(project);
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
        public async Task<IActionResult> AddProject([FromBody] ProjectRequestDTO projectRequest)
        {
            try
            {
                if (projectRequest == null)
                {
                    return BadRequest("Project data is required.");
                }
                var createdProject = await _projectService.AddProjectAsync(projectRequest);
                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectRequestDTO projectRequest)
        {
            try
            {
                if (projectRequest == null)
                {
                    return BadRequest("Project data is required.");
                }
                var updatedProject = await _projectService.UpdateProjectAsync(id, projectRequest);
                return Ok(updatedProject);
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
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                var deleted = await _projectService.DeleteProjectAsync(id);
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
