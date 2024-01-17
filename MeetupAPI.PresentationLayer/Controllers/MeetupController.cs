using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using MeetupAPI.Core.Exceptions.Validation;
using MeetupAPI.Core.Services.Abstractions;
using MeetupAPI.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace MeetupAPI.PresentationLayer.Controllers
{
    [Route("api/meetups")]
    [ApiController]
    [Authorize]
    public class MeetupController : ControllerBase
    {
        private readonly IMeetupService _meetupService;

        public MeetupController(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var meetups = await _meetupService.GetAllAsync();
            return Ok(meetups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var meetup = await _meetupService.GetByIdAsync(id);
            return Ok(meetup);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMeetupDto meetupForCreation)
        {
            var validationResult = new MeetupValidator().Validate(meetupForCreation);
            if (!validationResult.IsValid)
                throw new MeetupValidationException(validationResult.Errors.First().ErrorMessage);

            var meetup = await _meetupService.CreateAsync(meetupForCreation);
            return Created($"{HttpContext.Request.Path}", meetup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMeetupDto meetupForUpdate)
        {
            var validationResult = new MeetupValidator().Validate(meetupForUpdate);
            if (!validationResult.IsValid)
                throw new MeetupValidationException(validationResult.Errors.First().ErrorMessage);

            var meetup = await _meetupService.UpdateAsync(id, meetupForUpdate);
            return Ok(meetup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _meetupService.DeleteAsync(id);
            return NoContent();
        }
    }
}
