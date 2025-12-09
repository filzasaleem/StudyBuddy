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
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;

        public UserController(IUserService services)
        {
            _services = services;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var clerkUserId = User.FindFirst("sub")?.Value;
            var email = User.FindFirst("email")?.Value;
            var firstName = User.FindFirst("firstName")?.Value;
            var lastName = User.FindFirst("lastName")?.Value;
            if (clerkUserId == null || email == null)
                return Unauthorized();
            UserResponse user = await _services.GetOrCreateUserAsync(
                clerkUserId,
                email,
                firstName,
                lastName
            );
            return Ok(user);
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Authorized OK!");
        }
    }
}
