using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.GetDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services.Abstractions
{
    public interface IOrganizerService
    {
        Task<IEnumerable<GetOrganizerDto>> GetAllAsync();
        Task<GetOrganizerDto?> GetByIdAsync(Guid organizerId);
        Task<GetOrganizerDto> CreateAsync(CreateOrganizerDto newOrganizer);
        Task<GetOrganizerDto> UpdateAsync(Guid organizerId, UpdateOrganizerDto updatedOrganizer);
        Task DeleteAsync(Guid organizerId);
    }
}
