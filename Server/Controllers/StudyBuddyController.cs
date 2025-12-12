using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("*****************STUDY BUDDY CONTROLLER***************");
            Console.WriteLine("SEARCH QUERY: " + q);
            try
            {
                var cards = await _service.GetAllCardsAsync(q);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
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
