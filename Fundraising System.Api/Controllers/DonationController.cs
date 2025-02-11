using AutoMapper;
using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Application.UseCaseInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundraising_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;
        private readonly IMapper _mapper;

        public DonationController(IDonationService donationService, IMapper mapper)
        {
            _donationService = donationService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDonation([FromBody] DonationDto donationDto)
        {
            if (donationDto == null)
                return BadRequest("Donation data is null.");

            var createdDonation = await _donationService.CreateDonationAsync(donationDto);
            return CreatedAtAction(nameof(GetDonationById), new { id = createdDonation.Id }, createdDonation);
        }

        [Authorize(Roles = "Admin, Donor")]
        //[Authorize(Roles = "Admin")]
        // GET: api/donation
        [HttpGet]
        public async Task<ActionResult<List<DontionDtoResopnse>>> GetAllDonations()
        {
            var donations = await _donationService.GetAllDonationsAsync();
            return Ok(donations);
        }

        [Authorize(Roles = "Admin, Donor")]
        // GET: api/donation/project/{projectId}
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<List<DontionDtoResopnse>>> GetDonationsByProjectId(int projectId)
        {
            var donations = await _donationService.GetDonationsByProjectIdAsync(projectId);
            return Ok(donations);
        }

        // GET: api/donation/donor/{donorId}
        [HttpGet("donor/{donorId}")]
        public async Task<ActionResult<List<DontionDtoResopnse>>> GetDonationsByDonorId(string donorId)
        {
            var donations = await _donationService.GetDonationsByDonorIdAsync(donorId);
            return Ok(donations);
        }

        // GET: api/donation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DontionDtoResopnse>> GetDonationById(int id)
        {
            var donation = await _donationService.GetDonationByIdAsync(id);
            if (donation == null)
                return NotFound();

            return Ok(donation);
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/donation
        [HttpPut]
        public async Task<IActionResult> UpdateDonation([FromBody] DonationDto donationDto)
        {
            if (donationDto == null)
                return BadRequest("Donation data is null.");

            await _donationService.UpdateDonationAsync(donationDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/donation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            await _donationService.DeleteDonationAsync(id);
            return NoContent();
        }
    }
}
