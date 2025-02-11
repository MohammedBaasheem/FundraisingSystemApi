using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Domain.RepositoryInterfaces
{
    public interface IDonationRepository
    {
        Task<Donation?> CreateAsync(Donation donation);
        Task<List<Donation>?> GetAllAsync();
        Task<List<Donation>?> GetByProjectIdAsync(int projectId);
        Task<List<Donation>?> GetByDonorIdAsync(string donorId);
        Task<Donation?> GetByIdAsync(int id);
        Task UpdateAsync(Donation donation);
        Task DeleteAsync(int id);
    }
}
