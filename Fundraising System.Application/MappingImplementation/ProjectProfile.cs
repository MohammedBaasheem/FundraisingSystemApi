using AutoMapper;
using Fundraising_System.Application.DTOs.Requestes;
using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.MappingImplementation
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FinancialGoal, opt => opt.MapFrom(src => src.FinancialGoal))
                .ForMember(dest => dest.CurrentTotalDonations, opt => opt.Ignore());

            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FinancialGoal, opt => opt.MapFrom(src => src.FinancialGoal))
                .ForMember(dest => dest.CurrentTotalDonations, opt => opt.MapFrom(src => src.CurrentTotalDonations));

            CreateMap<Project,ProjectDtoResopnse>()
               .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.Donations))
               .ForMember(dest => dest.CurrentTotalDonations, opt => opt.MapFrom(src => src.CurrentTotalDonations))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.FinancialGoal, opt => opt.MapFrom(src => src.FinancialGoal));
        }
    }
}
