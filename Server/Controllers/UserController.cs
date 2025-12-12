using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;
using Servr.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;

        public UserController(IUserService services)
        {
            _services = services;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Console.WriteLine("*************Inside Controller********");
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst("email")?.Value;
            var firstName = User.FindFirst("firstName")?.Value;
            var lastName = User.FindFirst("lastName")?.Value;
            if (clerkUserId == null)
                return Unauthorized();
            UserResponse user = await _services.GetOrCreateUserAsync(
                clerkUserId,
                email,
                firstName,
                lastName
            );

            return Ok(user);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserUpdateRequest request)
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (clerkUserId == null)
                return Unauthorized();

            var updatedUser = await _services.UpdateUserAsync(clerkUserId, request);

            return Ok(updatedUser);
        }

        [HttpPost("ping")]
        public async Task<IActionResult> Ping()
        {
            Console.WriteLine("*****************PINGGGGGGGG CONTROLLER***************");
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId == null)
                return Unauthorized();

            await _services.TouchAsync(clerkUserId);

            return Ok(new { status = "updated" });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Authorized OK!");
        }
    }
}
