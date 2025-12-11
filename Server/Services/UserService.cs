using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server.DTOs;
using Server.Repositories;
using Servr.Models;

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

        public async Task TouchAsync(string clerkUserId)
        {
            var user = await _repo.GetUserByClerkIdAsync(clerkUserId);

            if (user == null)
                throw new Exception("User not found");
            await _repo.UpdateLastActiveAsync(user.Id);
        }

        public async Task<UserResponse> GetOrCreateUserAsync(
            string clerkUserId,
            string? email,
            string? firstName,
            string? lastName
        )
        {
            var user = await _repo.GetUserByClerkIdAsync(clerkUserId);
            if (user == null)
            {
                user = await _repo.CreateUserAsync(
                    new User
                    {
                        ClerkUserId = clerkUserId,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                    }
                );
            }
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> UpdateUserAsync(
            string clerkUserId,
            UserUpdateRequest request
        )
        {
            var user = await _repo.GetUserByClerkIdAsync(clerkUserId);

            if (user == null)
                throw new Exception("User not found");

            var updatedUser = await _repo.UpdateUserAsync(
                user.Id,
                request.FirstName,
                request.LastName,
                request.Email
            );

            return _mapper.Map<UserResponse>(updatedUser);
        }
    }
}
