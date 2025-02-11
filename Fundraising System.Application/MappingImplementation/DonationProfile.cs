using AutoMapper;
using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.MappingImplementation
{
    public class DonationProfile:Profile
    {
        public DonationProfile()
        {
            CreateMap<Donation,DonationDto>()
                .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.DonorId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => src.DonationDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            CreateMap<DonationDto, Donation>()
                .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.DonorId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => src.DonationDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));




            CreateMap<Donation, DontionDtoResopnse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.DonorId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => src.DonationDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            CreateMap<DontionDtoResopnse, Donation>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.DonorId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => src.DonationDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }
    }
}
