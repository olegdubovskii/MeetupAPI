using MeetupAPI.Core.DTOs.AuthDTOs;
using MeetupAPI.Core.Exceptions;
using MeetupAPI.Core.Models.Jwt;
using MeetupAPI.Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly SymmetricSecurityKey _secretKey;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        }

        public TokenPairDto GenerateJwtToken(IList<Claim> claims, IdentityUser<Guid> user)
        {
            return new TokenPairDto()
            {
                AccessToken = CreateToken(claims, user, _jwtOptions.AccessTokenExpirationTime),
                RefreshToken = CreateToken(claims, user, _jwtOptions.RefreshTokenExpirationTime),
            };
        }

        public ClaimsPrincipal GetClaimsPrincipalAndValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _secretKey,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                ClockSkew = TimeSpan.Zero
            };

            var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var validatedToken);
            if (validatedToken is null)
                throw new InvalidTokenException("Token validation was failed");
            
            return claimsPrincipal;
        }

        private Token CreateToken(IList<Claim> userClaims, IdentityUser<Guid> user, double expiredTime)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(expiredTime),
                signingCredentials: new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256));

            return new Token()
            {
                TokenValue = new JwtSecurityTokenHandler().WriteToken(token),
                CreationTime = token.ValidFrom,
                ExpiresTime = token.ValidTo
            };
        }
    }
}
