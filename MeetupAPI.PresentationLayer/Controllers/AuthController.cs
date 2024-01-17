using MeetupAPI.Core.DTOs.AuthDTOs;
using MeetupAPI.Core.Exceptions;
using MeetupAPI.Core.Models.Jwt;
using MeetupAPI.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.PresentationLayer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto user)
        {
            await _authService.SignUpAsync(user);
            return Created();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto user)
        {
            var token = await _authService.SignInAsync(user);
            Response.Cookies.Append("refreshToken", token.RefreshToken.TokenValue, new CookieOptions() { HttpOnly = true, Expires = token.RefreshToken.ExpiresTime });
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            string refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null)
                throw new RefreshTokenMissingException("Refresh token is missing from the cookies");

            var newRefreshToken = await _authService.RefreshTokenAsync(refreshToken);
            Response.Cookies.Append("refreshToken", newRefreshToken.RefreshToken.TokenValue, new CookieOptions() { HttpOnly = true, Expires = newRefreshToken.RefreshToken.ExpiresTime });

            return Ok(newRefreshToken);
        }
    }
}
