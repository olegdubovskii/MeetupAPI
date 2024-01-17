using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.DTOs.AuthDTOs
{
    public record class SignInDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
