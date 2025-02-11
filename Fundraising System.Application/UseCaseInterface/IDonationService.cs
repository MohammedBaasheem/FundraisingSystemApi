using Fundraising_System.Application.DTOs.Resopnseis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.UseCaseInterface
{
    public interface IDonationService
    {
        Task<DontionDtoResopnse> CreateDonationAsync(DonationDto donationDto);
        Task<List<DontionDtoResopnse>> GetAllDonationsAsync();
        Task<List<DontionDtoResopnse>> GetDonationsByProjectIdAsync(int projectId);
        Task<List<DontionDtoResopnse>> GetDonationsByDonorIdAsync(string donorId);
        Task<DontionDtoResopnse> GetDonationByIdAsync(int id);
        Task UpdateDonationAsync(DonationDto donationDto);
        Task DeleteDonationAsync(int id);
    }
}
