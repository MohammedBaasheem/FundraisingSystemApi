using Fundraising_System.Application.DTOs.Requestes;
using Fundraising_System.Application.DTOs.Resopnseis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.UseCaseInterface
{
    public interface IProjectService
    {

        Task<ProjectDtoResopnse> CreateProjectAsync(ProjectDto projectDto);
        Task<List<ProjectDtoResopnse>> GetAllProjectsAsync();
        Task<ProjectDtoResopnse> GetProjectByIdAsync(int id);
        Task<List<ProjectDtoResopnse>> GetProjectsByGoalAsync(decimal goal);
        Task UpdateProjectAsync(ProjectDto projectDto);
        Task DeleteProjectAsync(int id);
    }
}
