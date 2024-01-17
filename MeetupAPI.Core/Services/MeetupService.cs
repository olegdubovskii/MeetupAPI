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
    public class MeetupService : IMeetupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Meetup> _meetupRepository;
        private readonly IBaseRepository<Organizer> _organizerRepository;

        public MeetupService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _meetupRepository = _unitOfWork.GetRepository<Meetup>();
            _organizerRepository = _unitOfWork.GetRepository<Organizer>();
        }

        public async Task<IEnumerable<GetMeetupDto>> GetAllAsync()
        {
            var meetups = await _meetupRepository.GetItemsAsync();
            return _mapper.Map<IEnumerable<GetMeetupDto>>(meetups);
        }

        public async Task<GetMeetupDto?> GetByIdAsync(Guid meetupId)
        {
            var meetup = await _meetupRepository.GetItemByIDAsync(meetupId);
            if (meetup is null)
                throw new MeetupNotFoundException(meetupId);

            return _mapper.Map<GetMeetupDto>(meetup);
        }

        public async Task<GetMeetupDto?> CreateAsync(CreateMeetupDto newMeetup)
        {
            var organizer = await _organizerRepository.GetItemByIDAsync(newMeetup.OrganizerId);
            if (organizer is null)
                throw new OrganizerNotFoundException(newMeetup.OrganizerId);

            var mappedMeetup = _mapper.Map<Meetup>(newMeetup);
            var createdMeetup = await _meetupRepository.InsertItemAsync(mappedMeetup);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetMeetupDto>(createdMeetup);
        }

        public async Task<GetMeetupDto?> UpdateAsync(Guid meetupId, UpdateMeetupDto updatedMeetup)
        {
            var oldMeetup = await _meetupRepository.GetItemByIDAsync(meetupId);
            if (oldMeetup is null)
                throw new MeetupNotFoundException(meetupId);

            var organizer = await _organizerRepository.GetItemByIDAsync(updatedMeetup.OrganizerId);
            if (organizer is null)
                throw new OrganizerNotFoundException(updatedMeetup.OrganizerId);

            var mappedMeetup = _mapper.Map(updatedMeetup, oldMeetup);
            var resultMeetup = _meetupRepository.UpdateItem(mappedMeetup);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetMeetupDto>(resultMeetup);
        }

        public async Task DeleteAsync(Guid meetupId)
        {
            var existingMeetup = await _meetupRepository.GetItemByIDAsync(meetupId);
            if (existingMeetup is null)
                throw new MeetupNotFoundException(meetupId);

            _meetupRepository.DeleteItem(existingMeetup);
            await _unitOfWork.SaveAsync();  
        }
    }
}
