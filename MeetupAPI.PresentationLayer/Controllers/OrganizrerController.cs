using MeetupAPI.Core.DTOs.CreateDTOs;
using MeetupAPI.Core.DTOs.UpdateDTOs;
using MeetupAPI.Core.Exceptions.Validation;
using MeetupAPI.Core.Services;
using MeetupAPI.Core.Services.Abstractions;
using MeetupAPI.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.PresentationLayer.Controllers
{
    [Route("api/organizers")]
    [ApiController]
    [Authorize]
    public class OrganizrerController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizrerController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var organizers = await _organizerService.GetAllAsync();
            return Ok(organizers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var organizer = await _organizerService.GetByIdAsync(id);
            return Ok(organizer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrganizerDto organizerForCreation)
        {
            var validationResult = new OrganizerValidator().Validate(organizerForCreation);
            if (!validationResult.IsValid)
                throw new OrganizerValidationException(validationResult.Errors.First().ErrorMessage);

            var organizer = await _organizerService.CreateAsync(organizerForCreation);
            return Created($"{HttpContext.Request.Path}", organizer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateOrganizerDto organizerForUpdate)
        {
            var validationResult = new OrganizerValidator().Validate(organizerForUpdate);
            if (!validationResult.IsValid)
                throw new OrganizerValidationException(validationResult.Errors.First().ErrorMessage);

            var organizer = await _organizerService.UpdateAsync(id, organizerForUpdate);
            return Ok(organizer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _organizerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
