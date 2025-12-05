using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            IUserService userService,
            IJwtTokenService tokenServices,
            IMapper mapper
        )
        {
            _userService = userService;
            _tokenService = tokenServices;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> LogIn(LoginRequest request)
        {
            try
            {
                var user = await _userService.ValidateUser(request);
                var token = _tokenService.GenerateToken(user);
                var mappedUser = _mapper.Map<UserResponse>(user);
                return Ok(new AuthResponse { User = mappedUser, Token = token });
            }
            catch (System.Exception)
            {
                return Unauthorized("Invalid Email or password");
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserResponse>> SignUp([FromBody] UserRequest request)
        {
            var user = await _userService.AddUser(request);
            return Ok(user);
        }
    }
}
