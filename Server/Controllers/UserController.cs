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
            Console.WriteLine("*************Inside Controller********");
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst("email")?.Value;
            var firstName = User.FindFirst("firstName")?.Value;
            var lastName = User.FindFirst("lastName")?.Value;
            Console.WriteLine("clerk id is: " + clerkUserId);
            Console.WriteLine("****************");
            Console.WriteLine("Email is: " + email);
            Console.WriteLine("****************");
            Console.WriteLine("FirstName is: " + firstName);
            Console.WriteLine("****************");
            Console.WriteLine("Last Name is: " + lastName);
            Console.WriteLine("****************");

            // if (clerkUserId == null || email == null)
            //     return Unauthorized();
            UserResponse user = await _services.GetOrCreateUserAsync(
                clerkUserId,
                email,
                firstName,
                lastName
            );
            Console.WriteLine("***********************USER***************");
            Console.WriteLine(user);
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
