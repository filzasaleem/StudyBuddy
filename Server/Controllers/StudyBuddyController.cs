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
    [Route("api/studybuddy")]
    [Authorize]
    public class StudyBuddyController : ControllerBase
    {
        private readonly IStudyBuddyService _service;

        public StudyBuddyController(IStudyBuddyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudyBuddyCardResponse>>> GetAllCards(
            [FromQuery] string? q
        )
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(clerkUserId))
                return Unauthorized();

            var cards = await _service.GetAllCardsAsync(q, clerkUserId);
            return Ok(cards);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<StudyBuddyCardResponse>>> GetCardsByUser(Guid userId)
        {
            var cards = await _service.GetCardsByUserIdAsync(userId);
            if (cards.Count == 0)
                return NotFound();
            return Ok(cards);
        }
    }
}
