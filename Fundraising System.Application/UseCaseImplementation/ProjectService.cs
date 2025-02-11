using AutoMapper;
using Fundraising_System.Application.DTOs.Requestes;
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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectDtoResopnse> CreateProjectAsync(ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("Received null projectDto");
                throw new BadRequestException("Project data cannot be null.");
            }

            _logger.LogInformation("Creating a new project: {@ProjectDto}", projectDto);
            var project = _mapper.Map<Project>(projectDto);
            var createdProject = await _projectRepository.CreateAsync(project);
            _logger.LogInformation("Project created successfully with ID: {ProjectId}", createdProject.Id);
            return _mapper.Map<ProjectDtoResopnse>(createdProject);
        }

        public async Task<List<ProjectDtoResopnse>> GetAllProjectsAsync()
        {
            _logger.LogInformation("Retrieving all projects");
            var projects = await _projectRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {ProjectCount} projects", projects.Count);
            return _mapper.Map<List<ProjectDtoResopnse>>(projects);
        }

        public async Task<ProjectDtoResopnse> GetProjectByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid project ID: {ProjectId}", id);
                throw new BadRequestException("Project ID must be greater than zero.");
            }

            _logger.LogInformation("Retrieving project with ID: {ProjectId}", id);
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning("No project found with ID: {ProjectId}", id);
                throw new NotFoundException("No Project by This Id!");
            }
            _logger.LogInformation("Project retrieved successfully: {@Project}", project);
            return _mapper.Map<ProjectDtoResopnse>(project);
        }

        public async Task<List<ProjectDtoResopnse>> GetProjectsByGoalAsync(decimal goal)
        {
            if (goal <= 0)
            {
                _logger.LogWarning("Invalid goal: {Goal}", goal);
                throw new BadRequestException("Goal must be greater than zero.");
            }

            _logger.LogInformation("Retrieving projects with goal: {Goal}", goal);
            var projects = await _projectRepository.GetByGoalAsync(goal);
            if (projects == null || !projects.Any())
            {
                _logger.LogWarning("No projects found with goal: {Goal}", goal);
                throw new NotFoundException("No Project by This goal!");
            }
            _logger.LogInformation("Retrieved {ProjectCount} projects with goal: {Goal}", projects.Count, goal);
            return _mapper.Map<List<ProjectDtoResopnse>>(projects);
        }

        public async Task UpdateProjectAsync(ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("Received null projectDto");
                throw new BadRequestException("Project data cannot be null.");
            }

            _logger.LogInformation("Updating project: {@ProjectDto}", projectDto);
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.UpdateAsync(project);
            _logger.LogInformation("Project updated successfully with ID: {ProjectId}", project.Id);
        }

        public async Task DeleteProjectAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid project ID: {ProjectId}", id);
                throw new BadRequestException("Project ID must be greater than zero.");
            }

            _logger.LogInformation("Deleting project with ID: {ProjectId}", id);
            await _projectRepository.DeleteAsync(id);
            _logger.LogInformation("Project deleted successfully with ID: {ProjectId}", id);
        }
    }
}