using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Entities
{
    public class Meetup
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
        public string Location { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}
