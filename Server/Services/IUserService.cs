using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOs;

namespace Server.Services
{
    public interface IUserService
    {
        public Task<UserResponse> GetOrCreateUserAsync(
            string clerkUserId,
            string? email,
            string? firstName,
            string? lastName
        );
    }
}
