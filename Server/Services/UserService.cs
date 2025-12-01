using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server.DTOs;
using Server.Repositories;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserResponse> AddUser(UserRequest user)
        {
            var newUser = await _repo.CreateUserAsync(
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password
            );
            return _mapper.Map<UserResponse>(newUser);
        }

        public async Task<User> ValidateUser(LoginRequest request)
        {
            var user =
                await _repo.ValidateUserAsync(request.Email, request.Password)
                ?? throw new Exception("Error occured");
            return user;
        }
    }
}
