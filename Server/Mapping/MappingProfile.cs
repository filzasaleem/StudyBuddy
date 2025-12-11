using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server.DTOs;
using Servr.Models;

namespace Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Event, EventDto>();
            CreateMap<User, StudyBuddyCardResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim())
                )
                .ForMember(
                    dest => dest.Initials,
                    opt =>
                        opt.MapFrom(src =>
                            $"{(src.FirstName.Length > 0 ? src.FirstName[0] : ' ')}"
                            + $"{(src.LastName.Length > 0 ? src.LastName[0] : ' ')}".ToUpper()
                        )
                )
                .ForMember(dest => dest.Subject, opt => opt.Ignore()) 
                .ForMember(dest => dest.IsOnline, opt => opt.Ignore());
        }
    }
}
