using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.DTOs.DefaultDTOs
{
    public record class DefaultMeetupDto
    {
        public string Title { get; init; }
        public string? Description { get; init; }
        public Guid OrganizerId { get; init; }
        public string? Location { get; init; }
        public DateTime? DateAndTime { get; init; }
    }
}
