using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Entities
{
    public class Organizer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Meetup>? Meetups { get; set; }
    }
}
