using MeetupAPI.Core.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services.Abstractions
{
    public interface IAuthService
    {
        Task SignUpAsync(SignUpDto userInfo);
        Task<TokenPairDto> SignInAsync(SignInDto userInfo);
        Task<TokenPairDto> RefreshTokenAsync(string refreshToken);
    }
}
