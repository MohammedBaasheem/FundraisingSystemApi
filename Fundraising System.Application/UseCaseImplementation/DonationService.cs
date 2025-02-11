using AutoMapper;
using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Application.Exceptions;
using Fundraising_System.Application.UseCaseInterface;
using Fundraising_System.Domain.Entities;
using Fundraising_System.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.UseCaseImplementation
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DonationService> _logger; 

        public DonationService(IDonationRepository donationRepository, IMapper mapper, ILogger<DonationService> logger)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
            _logger = logger; 
        }

        public async Task<DontionDtoResopnse> CreateDonationAsync(DonationDto donationDto)
        {
            if (donationDto == null)
            {
                _logger.LogWarning("Received null donationDto");
                throw new BadRequestException("Donation data cannot be null.");
            }

            var donation = _mapper.Map<Donation>(donationDto);
            var createdDonation = await _donationRepository.CreateAsync(donation);

            _logger.LogInformation("Donation created successfully with ID: {DonationId}", createdDonation.Id); 
            return _mapper.Map<DontionDtoResopnse>(createdDonation);
        }

        public async Task<List<DontionDtoResopnse>> GetAllDonationsAsync()
        {
            _logger.LogInformation("Retrieving all donations");
            var donations = await _donationRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {DonationCount} donations", donations.Count);
            return _mapper.Map<List<DontionDtoResopnse>>(donations);
        }

        public async Task<List<DontionDtoResopnse>> GetDonationsByProjectIdAsync(int projectId)
        {
            if (projectId <= 0)
            {
                _logger.LogWarning("Invalid project ID: {ProjectId}", projectId);
                throw new BadRequestException("Project ID must be greater than zero.");
            }

            _logger.LogInformation("Retrieving donations for project ID: {ProjectId}", projectId);
            var donations = await _donationRepository.GetByProjectIdAsync(projectId);
            if (donations == null || !donations.Any())
            {
                _logger.LogWarning("No donations found for project ID: {ProjectId}", projectId);
                throw new NotFoundException("No donations found for this project ID.");
            }
            return _mapper.Map<List<DontionDtoResopnse>>(donations);
        }

        public async Task<List<DontionDtoResopnse>> GetDonationsByDonorIdAsync(string donorId)
        {
            if (string.IsNullOrWhiteSpace(donorId))
            {
                _logger.LogWarning("Received null or empty donor ID");
                throw new BadRequestException("Donor ID cannot be null or empty.");
            }

            _logger.LogInformation("Retrieving donations for donor ID: {DonorId}", donorId);
            var donations = await _donationRepository.GetByDonorIdAsync(donorId);
            if (donations == null || !donations.Any())
            {
                _logger.LogWarning("No donations found for donor ID: {DonorId}", donorId);
                throw new NotFoundException("No donations found for this donor ID.");
            }
            return _mapper.Map<List<DontionDtoResopnse>>(donations);
        }

        public async Task<DontionDtoResopnse> GetDonationByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid donation ID: {DonationId}", id);
                throw new BadRequestException("Donation ID must be greater than zero.");
            }

            _logger.LogInformation("Retrieving donation with ID: {DonationId}", id);
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                _logger.LogWarning("No donation found with ID: {DonationId}", id);
                throw new NotFoundException("No donation found by this ID.");
            }
            return _mapper.Map<DontionDtoResopnse>(donation);
        }

        public async Task UpdateDonationAsync(DonationDto donationDto)
        {
            if (donationDto == null)
            {
                _logger.LogWarning("Received null donationDto");
                throw new BadRequestException("Donation data cannot be null.");
            }

            var donation = new Donation
            {
                DonorId = donationDto.DonorId,
                Amount = donationDto.Amount,
                DonationDate = donationDto.DonationDate,
                ProjectId = donationDto.ProjectId
            };

            await _donationRepository.UpdateAsync(donation);
            _logger.LogInformation("Donation updated successfully with ID: {DonationId}", donation.Id); 
        }

        public async Task DeleteDonationAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid donation ID: {DonationId}", id);
                throw new BadRequestException("Donation ID must be greater than zero.");
            }

            _logger.LogInformation("Deleting donation with ID: {DonationId}", id);
            await _donationRepository.DeleteAsync(id);
            _logger.LogInformation("Donation deleted successfully with ID: {DonationId}", id); 
        }
    }
}