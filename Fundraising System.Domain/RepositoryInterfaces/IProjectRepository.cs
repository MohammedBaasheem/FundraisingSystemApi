using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Domain.RepositoryInterfaces
{
    public interface IProjectRepository
    {
        Task<Project?> CreateAsync(Project project);
        Task<List<Project>?> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<List<Project>?> GetByGoalAsync(decimal goal);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}
