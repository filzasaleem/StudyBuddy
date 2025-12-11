using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Servr.Models;

namespace Server.Repositories
{
    public interface IUserRepo
    {
        public Task UpdateLastActiveAsync(Guid userId);
        public Task<User?> GetUserByClerkIdAsync(string clerkUserId);
        public Task<User> CreateUserAsync(User user);
        public Task<User> UpdateUserAsync(
            Guid id,
            string? firstName,
            string? lastName,
            string? email
        );
    }
}
