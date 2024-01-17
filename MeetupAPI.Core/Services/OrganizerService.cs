using AutoMapper;
using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.GetDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using MeetupAPI.Core.Exceptions.NotFound;
using MeetupAPI.Core.Services.Abstractions;
using MeetupAPI.DAL.Entities;
using MeetupAPI.DAL.Repositories.Abstractions;
using MeetupAPI.DAL.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Organizer> _organizerRepository;

        public OrganizerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _organizerRepository = _unitOfWork.GetRepository<Organizer>();
        }

        public async Task<IEnumerable<GetOrganizerDto>> GetAllAsync()
        {
            var organizers = await _organizerRepository.GetItemsAsync();
            return _mapper.Map<IEnumerable<GetOrganizerDto>>(organizers);
        }

        public async Task<GetOrganizerDto?> GetByIdAsync(Guid organizerId)
        {
            var organizer = await _organizerRepository.GetItemByIDAsync(organizerId);
            if (organizer is null)
                throw new OrganizerNotFoundException(organizerId);

            return _mapper.Map<GetOrganizerDto>(organizer);
        }

        public async Task<GetOrganizerDto> CreateAsync(CreateOrganizerDto newOrganizer)
        {
            var mappedOrganizer = _mapper.Map<Organizer>(newOrganizer);

            var createdOrganizer = await _organizerRepository.InsertItemAsync(mappedOrganizer);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetOrganizerDto>(createdOrganizer);
        }

        public async Task<GetOrganizerDto> UpdateAsync(Guid organizerId, UpdateOrganizerDto updatedOrganizer)
        {
            var oldOrganizer = await _organizerRepository.GetItemByIDAsync(organizerId);
            if (oldOrganizer is null)
                throw new OrganizerNotFoundException(organizerId);

            var mappedOrganizer = _mapper.Map(updatedOrganizer, oldOrganizer);
            var resultOrganizer = _organizerRepository.UpdateItem(mappedOrganizer);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetOrganizerDto>(resultOrganizer);
        }

        public async Task DeleteAsync(Guid organizerId)
        {
            var existingOrganizer = await _organizerRepository.GetItemByIDAsync(organizerId);
            if (existingOrganizer is null)
                throw new OrganizerNotFoundException(organizerId);

            _organizerRepository.DeleteItem(existingOrganizer);
            await _unitOfWork.SaveAsync();
        }
    }
}
