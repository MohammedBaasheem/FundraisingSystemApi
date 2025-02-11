using Fundraising_System.Domain.Entities;
using Fundraising_System.Domain.RepositoryInterfaces;
using Fundraising_System.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Infrastructure.RepositoryImplementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<List<Project>?> GetAllAsync()
        {
            return await _context.Projects.Include(p=>p.Donations).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.Include(p=>p.Donations).FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<List<Project>?> GetByGoalAsync(decimal goal)
        {
            return await _context.Projects.Include(p => p.Donations)
                .Where(p => p.FinancialGoal <= goal)
                .ToListAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await GetByIdAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}
