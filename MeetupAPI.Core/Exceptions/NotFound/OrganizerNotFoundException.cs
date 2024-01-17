using MeetupAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions.NotFound
{
    public class OrganizerNotFoundException : NotFoundException
    {
        public OrganizerNotFoundException(Guid organizerId) : base($"Organizer with id: {organizerId} was not found") { }
    }
}
