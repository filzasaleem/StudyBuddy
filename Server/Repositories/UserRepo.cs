using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;

namespace Server.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly StudyBiddyDbContext _context;

        public UserRepo(StudyBiddyDbContext context)
        {
            _context = context;
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
    }
}
