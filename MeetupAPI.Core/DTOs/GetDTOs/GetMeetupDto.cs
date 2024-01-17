using MeetupAPI.Core.DTOs.DefaultDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.DTOs.GetDTOs
{
    public record class GetMeetupDto : DefaultMeetupDto
    {
        public Guid Id { get; init; }
    }
}
