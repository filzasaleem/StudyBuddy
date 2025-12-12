using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs;
using Servr.Models;

namespace Server.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly StudyBuddyDbContext _context;

        public UserRepo(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public async Task UpdateLastActiveAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return;

            user.LastActiveAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByClerkIdAsync(string clerkUserId)
        {
            return await _context.Users.FirstOrDefaultAsync(user =>
                user.ClerkUserId == clerkUserId
            );
        }

        public async Task<User> UpdateUserAsync(
            Guid id,
            string? firstName,
            string? lastName,
            string? email
        )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrWhiteSpace(firstName))
                user.FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName))
                user.LastName = lastName;

            if (!string.IsNullOrWhiteSpace(email))
                user.Email = email;
            user.LastActiveAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
