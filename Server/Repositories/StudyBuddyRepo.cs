using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Servr.Models;

namespace Server.Repositories
{
    public class StudyBuddyRepo : IStudyBuddyRepo
    {
        private readonly StudyBuddyDbContext _context;

        public StudyBuddyRepo(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersWithEventsAsync()
        {
            return await _context
                .Users.Include(u => u.Events) // eager load events
                .ToListAsync();
        }

        public async Task<User?> GetUserWithEventsByIdAsync(Guid userId)
        {
            return await _context
                .Users.Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
