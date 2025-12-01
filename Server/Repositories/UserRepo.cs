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
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepo(StudyBiddyDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password
        )
        {
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;
        }
    }
}
