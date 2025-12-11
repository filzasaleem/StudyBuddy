using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/studybuddy")]
    public class StudyBuddyController : ControllerBase
    {
        private readonly IStudyBuddyService _service;

        public StudyBuddyController(IStudyBuddyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudyBuddyCardResponse>>> GetAllCards()
        {
            var cards = await _service.GetAllCardsAsync();
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
