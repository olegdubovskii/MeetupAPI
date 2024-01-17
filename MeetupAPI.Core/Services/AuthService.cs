using AutoMapper;
using MeetupAPI.Core.DTOs.AuthDTOs;
using MeetupAPI.Core.Exceptions;
using MeetupAPI.Core.Exceptions.NotFound;
using MeetupAPI.Core.Models.Jwt;
using MeetupAPI.Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager, 
            ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task SignUpAsync(SignUpDto userInfo)
        {
            var identityUser = _mapper.Map<IdentityUser<Guid>>(userInfo);

            IdentityResult userCreationResult = await _userManager.CreateAsync(identityUser, userInfo.Password);
            if (!userCreationResult.Succeeded)
                throw new UserSignUpException(userCreationResult.Errors.First().Description);
            
            await _signInManager.SignInAsync(identityUser, false);
            var userIdClaim = new Claim(ClaimTypes.Actor, identityUser.Id.ToString());
            await _userManager.AddClaimAsync(identityUser, userIdClaim);
        }

        public async Task<TokenPairDto> SignInAsync(SignInDto userInfo)
        {
            var requiredUser = await _userManager.FindByEmailAsync(userInfo.Email);
            if (requiredUser is null)
                throw new UserNotFoundException("User was not found");

            var result = await _signInManager.PasswordSignInAsync(requiredUser, userInfo.Password, false, false);
            if (!result.Succeeded)
                throw new SignInWasFailedException("SignIn was failed");

            var userClaims = await _userManager.GetClaimsAsync(requiredUser);
            return _tokenService.GenerateJwtToken(userClaims, requiredUser);
        }

        public async Task<TokenPairDto> RefreshTokenAsync(string refreshToken)
        {
            var claimsPrincipal = _tokenService.GetClaimsPrincipalAndValidateToken(refreshToken);

            string userId = claimsPrincipal.Claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Actor))!.Value;
            var detectedUser = await _userManager.FindByIdAsync(userId);
            if (detectedUser is null)
                throw new UserNotFoundException("User was not found");

            var userClaims = await _userManager.GetClaimsAsync(detectedUser);

            return _tokenService.GenerateJwtToken(userClaims, detectedUser);
        }
    }
}
