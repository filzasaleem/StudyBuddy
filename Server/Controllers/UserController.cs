using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUsers()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<UserResponse>> AddUser()
    }
}
