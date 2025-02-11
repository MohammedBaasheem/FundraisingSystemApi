using AutoMapper;
using Fundraising_System.Application.DTOs.Requestes;
using Fundraising_System.Application.UseCaseInterface;
using Fundraising_System.Application.DTOs.Resopnseis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundraising_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        // POST: api/projects
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<ProjectDtoResopnse>> CreateProjectAsync([FromBody] ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            var createdProject = await _projectService.CreateProjectAsync(projectDto);

            if (createdProject == null)
            {
                return BadRequest("Failed to create project.");
            }

            
            return  createdProject;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectDtoResopnse>>> GetAllProjectsAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDtoResopnse>> GetProjectByIdAsync(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // GET: api/projects/goal/{goal}
        [HttpGet("goal/{goal}")]
        public async Task<ActionResult<List<ProjectDtoResopnse>>> GetProjectsByGoalAsync(decimal goal)
        {
            var projects = await _projectService.GetProjectsByGoalAsync(goal);
            return Ok(projects);
        }

        // PUT: api/projects
        [HttpPut]
        public async Task<IActionResult> UpdateProjectAsync([FromBody] ProjectDto projectDto)
        {
            await _projectService.UpdateProjectAsync(projectDto);
            return NoContent();
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
