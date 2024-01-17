using MeetupAPI.Core.Models.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.DTOs.AuthDTOs
{
    public record class TokenPairDto
    {
        public Token AccessToken { get; init; }
        public Token RefreshToken { get; init; }
    }
}
