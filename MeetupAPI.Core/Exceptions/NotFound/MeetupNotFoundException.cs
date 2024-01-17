using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions.NotFound
{
    public class MeetupNotFoundException : NotFoundException
    {
        public MeetupNotFoundException(Guid meetupId) : base($"Meetup with id: {meetupId} was not found") { }
    }
}
