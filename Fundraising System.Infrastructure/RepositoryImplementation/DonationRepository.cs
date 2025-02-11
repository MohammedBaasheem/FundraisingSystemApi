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
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Donation?> CreateAsync(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            return donation;
        }

        public async Task<List<Donation>?> GetAllAsync()
        {
            return await _context.Donations.AsNoTracking().ToListAsync();
        }

        public async Task<List<Donation>?> GetByProjectIdAsync(int projectId)
        {
            return await _context.Donations
                .Where(d => d.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<List<Donation>?> GetByDonorIdAsync(string donorId)
        {
            return await _context.Donations
                .Where(d => d.DonorId == donorId)
                .ToListAsync();
        }

        public async Task<Donation?> GetByIdAsync(int id)
        {
            return await _context.Donations.FindAsync(id);
        }

        public async Task UpdateAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donation = await GetByIdAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
