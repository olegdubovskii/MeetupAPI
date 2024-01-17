using AutoMapper;
using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.GetDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using MeetupAPI.DAL.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services.Abstractions
{
    public interface IMeetupService
    {
        Task<IEnumerable<GetMeetupDto>> GetAllAsync();
        Task<GetMeetupDto?> GetByIdAsync(Guid meetupId);
        Task<GetMeetupDto?> CreateAsync(CreateMeetupDto newMeetup);
        Task<GetMeetupDto?> UpdateAsync(Guid meetupId, UpdateMeetupDto updatedMeetup);
        Task DeleteAsync(Guid meetupId);  
    }
}
