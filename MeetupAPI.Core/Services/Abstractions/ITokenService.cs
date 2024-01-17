using MeetupAPI.Core.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services.Abstractions
{
    public interface ITokenService
    {
        TokenPairDto GenerateJwtToken(IList<Claim> claims, IdentityUser<Guid> user);
        ClaimsPrincipal GetClaimsPrincipalAndValidateToken(string token);

    }
}
