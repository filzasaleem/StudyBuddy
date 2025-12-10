using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/events")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId == null)
                return Unauthorized();
            return Ok(await _eventService.GetEventsAsync(clerkUserId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto request)
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId == null)
                return Unauthorized();
            var createEvent = await _eventService.CreateEventAsync(clerkUserId, request);
            return CreatedAtAction(nameof(GetEvents), new { id = createEvent.Id }, createEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId == null)
                return Unauthorized();
            var deleted = await _eventService.DeleteEventAsync(clerkUserId, id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventCreateDto request)
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId == null)
                return Unauthorized();
            var updatedEvent = await _eventService.UpdateEventAsync(clerkUserId, id, request);
            if (updatedEvent == null)
                return NotFound();

            return Ok(updatedEvent);
        }
    }
}
