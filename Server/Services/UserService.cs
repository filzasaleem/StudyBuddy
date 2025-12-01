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

        public Task<List<UserResponse>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
