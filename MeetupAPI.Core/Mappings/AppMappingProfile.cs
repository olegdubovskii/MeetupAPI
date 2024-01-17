using AutoMapper;
using MeetupAPI.Core.DTOs.AuthDTOs;
using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.GetDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using MeetupAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Mappings
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CreateMeetupDto, Meetup>().ReverseMap();
            CreateMap<CreateOrganizerDto, Organizer>().ReverseMap();
            CreateMap<GetMeetupDto, Meetup>().ReverseMap();
            CreateMap<GetOrganizerDto, Organizer>().ReverseMap();
            CreateMap<UpdateMeetupDto, Meetup>().ReverseMap();
            CreateMap<UpdateOrganizerDto, Organizer>().ReverseMap();
            CreateMap<IdentityUser<Guid>, SignUpDto>().ReverseMap();
            CreateMap<IdentityUser<Guid>, SignInDto>().ReverseMap();
        }
    }
}
